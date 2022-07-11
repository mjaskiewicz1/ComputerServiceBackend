using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerService.Backend.Dtos;
using ComputerService.Backend.Interfaces;
using Data.Context;
using Data.Enums;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using RequestAF.Dtos;

namespace ComputerService.Backend.Services;

public class RequestService : IRequestService
{
    private readonly ComputerServiceContext _context;

    public RequestService(ComputerServiceContext context)
    {
        _context = context;
    }

    private string RmaGenerator => DateTime.UtcNow.ToString("yMddHHmmssff");

    public async Task<RequestDto?> GetRequestAndRequestConversation(string rma)
    {
        try
        {
            var model = await _context.Requests.Select(r => new RequestDto
            {
                Rma = r.Rma,
                EmployeeObjectId = r.EmployeeObjectId,
                Email = r.Email,
                RequestStatus = r.RequestStatus,
                Details = r.Details,
                Url = r.Url,
                Name = r.Name,
                Surname = r.Surname
            }).AsNoTracking().FirstOrDefaultAsync(r => r.Rma == rma);
            if (model == null) return null;

            var requestConversations = await (from requestConversation in _context.RequestConversations
                                              where requestConversation.Rma == rma
                                              select new RequestConversationDto
                                              {
                                                  CreatedDateTime = requestConversation.CreatedDateTime,
                                                  EmployeeObjectId = requestConversation.EmployeeObjectId,
                                                  Message = requestConversation.Message
                                              }).OrderByDescending(r => r.CreatedDateTime).AsNoTracking().OrderBy(r => r.CreatedDateTime)
                .ToListAsync();
            model.RequestConversations = requestConversations;
            return model;
        }
        catch (Exception e)
        {
            throw new Exception();
        }
    }


    public async Task<Request> CreateAsync(Request model)
    {
        var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var rmaNew = RmaGenerator;
            model.Rma = rmaNew;
            model.Url = Guid.NewGuid();
            model.RequestInvoiceDetail.Rma = rmaNew;
            if (model.RequestShipmentDetail != null) model.RequestInvoiceDetail.Rma = rmaNew;


            await _context.Requests.AddAsync(model);


            await _context.SaveChangesAsync();

            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"INSERT INTO RequestConversations (Message,Rma) values('Twoje zlecenie zostało zarejestrowane',{rmaNew})");
            await _context.SaveChangesAsync();
            // Commit transaction if all commands succeed, transaction will auto-rollback
            // when disposed if either commands fails
            await transaction.CommitAsync();
            return model;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Request> GetAsync(string rma)
    {
        return await _context.Requests.Include(x => x.RequestInvoiceDetail)
            .Include(x => x.RequestShipmentDetail)
            .Select(d => new Request
            {
                Rma = d.Rma,
                EmployeeObjectId = d.EmployeeObjectId,
                Url = d.Url,
                Name = d.Name,
                Surname = d.Surname,
                PhoneNumber = d.PhoneNumber,
                Email = d.Email,
                RequestStatus = d.RequestStatus,
                Brand = d.Brand,
                Model = d.Model,
                SerialNumber = d.SerialNumber,
                Details = d.Details,
                CreatedDateTime = d.CreatedDateTime,
                FailureDescription = d.FailureDescription,
                RequestShipmentDetail = d.RequestShipmentDetail,
                RequestInvoiceDetail = d.RequestInvoiceDetail
            }).AsNoTracking().OrderByDescending(d => d.CreatedDateTime).FirstOrDefaultAsync(r => r.Rma == rma);
    }

    public async Task<RequestDto?> GetAsync(string rma, Guid url)
    {
        var model = await _context.Requests.Include(x => x.RequestInvoiceDetail)
            .Include(x => x.RequestShipmentDetail).Include(x => x.Invoice)
            .Select(d => new RequestDto
            {
                InvoiceNumber = d.Invoice.Number,
                Rma = d.Rma,
                EmployeeObjectId = d.EmployeeObjectId,
                Url = d.Url,
                Name = d.Name,
                Surname = d.Surname,
                PhoneNumber = d.PhoneNumber,
                Email = d.Email,
                RequestStatus = d.RequestStatus,
                Brand = d.Brand,
                Model = d.Model,
                SerialNumber = d.SerialNumber,
                Details = d.Details,
                FailureDescription = d.FailureDescription,
                RequestShipment = new RequestShipmentDto(d.RequestShipmentDetail.City, d.RequestShipmentDetail.Postcode,
                    d.RequestShipmentDetail.Street),
                RequestInvoiceDetail = new RequestInvoiceDetailDto(d.RequestInvoiceDetail.City,
                    d.RequestInvoiceDetail.Name, d.RequestInvoiceDetail.NameCompany, d.RequestInvoiceDetail.Nip,
                    d.RequestInvoiceDetail.Postcode, d.RequestInvoiceDetail.Street, d.RequestInvoiceDetail.Surname)
            }).AsNoTracking().FirstOrDefaultAsync(r => r.Rma == rma && r.Url == url);
        var requestConversations = await (from requestConversation in _context.RequestConversations
                                          where requestConversation.Rma == rma
                                          select new RequestConversationDto
                                          {
                                              CreatedDateTime = requestConversation.CreatedDateTime,
                                              EmployeeObjectId = requestConversation.EmployeeObjectId,
                                              Message = requestConversation.Message
                                          }).OrderByDescending(r => r.CreatedDateTime).AsNoTracking().OrderBy(r => r.CreatedDateTime).ToListAsync();
        model.RequestConversations = requestConversations;

        return model;
    }

    public async Task<Request> UpdateAsync(RequestDto model
    )
    {
        var request = await _context.Requests.FindAsync(model.Rma);
        if (request != null)
        {
            request.RequestStatus = (RequestStatus)model.RequestStatus;
            if (request.RequestStatus == RequestStatus.Wycenione)
            {
                request.Estimate = model.Estimate;
            }

            request.EmployeeObjectId = model.EmployeeObjectId;
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }

        return null;
    }

    public async Task<IEnumerable<Request>> GetAllAsync()
    {
        return await _context.Requests.Select(r => new Request
        {
            Rma = r.Rma,
            EmployeeObjectId = r.EmployeeObjectId,
            Name = r.Name,
            Surname = r.Surname,
            Email = r.Email,
            PhoneNumber = r.PhoneNumber,
            RequestStatus = r.RequestStatus,
            CreatedDateTime = r.CreatedDateTime
        }).AsNoTracking().OrderByDescending(r => r.CreatedDateTime).ToListAsync();
    }

    public async Task<IEnumerable<RequestHistoryDto>> GetHistoryAll(string rma)
    {
        return await _context.Requests.TemporalAll().Where(r => r.Rma == rma).Select(r => new RequestHistoryDto
        {
            RequestStatus = r.RequestStatus.ToString(),
            PeriodStart = EF.Property<DateTime>(r, "PeriodStart")
        }).AsNoTracking().OrderBy(rh => rh.PeriodStart).ToListAsync();
    }

    public async Task<bool> DeleteAsync(string rma)
    {
        var model = await _context.Requests.FirstOrDefaultAsync(r => r.Rma == rma);
        if (model == null) return false;

        _context.Requests.Remove(model);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Request>> FilterAsync(RequestFilterDto model)
    {
        IQueryable<Request> request = _context.Requests.Select(r => new Request
        {
            Rma = r.Rma,
            EmployeeObjectId = r.EmployeeObjectId,
            Name = r.Name,
            Surname = r.Surname,
            Email = r.Email,
            PhoneNumber = r.PhoneNumber,
            RequestStatus = r.RequestStatus,
            CreatedDateTime = r.CreatedDateTime
        }).OrderByDescending(r => r.CreatedDateTime);

        filterEmail(model, ref request);
        filterRequestStatus(model, ref request);
        filterDate(model, ref request);
        return await request.ToListAsync();
    }

    private void filterEmail(RequestFilterDto model, ref IQueryable<Request> requestParameter)
    {
        if (model.Email != null) requestParameter = requestParameter.Where(r => r.Email == model.Email);
    }

    private void filterRequestStatus(RequestFilterDto model, ref IQueryable<Request> requestParameter)
    {
        if (model.RequestStatus != null && model.RequestStatus.Any())
            requestParameter = requestParameter.Where(r => model.RequestStatus.Contains(r.RequestStatus));
    }

    private void filterDate(RequestFilterDto model, ref IQueryable<Request> requestParameter)
    {
        if (model.CreateDateTimeTo == null && model.CreateDateTimeFrom == null) return;
        if (model.CreateDateTimeFrom != null && model.CreateDateTimeTo != null)
            requestParameter = requestParameter.Where(r =>
                r.CreatedDateTime >= model.CreateDateTimeFrom &&
                r.CreatedDateTime.Date <= model.CreateDateTimeTo.Value.Date);
        else if (model.CreateDateTimeFrom != null)
            requestParameter =
                requestParameter.Where(r => r.CreatedDateTime.Date >= model.CreateDateTimeFrom.Value.Date);
        else
            requestParameter = requestParameter.Where(r => r.CreatedDateTime.Date <= model.CreateDateTimeTo.Value.Date);
    }
    public async Task<bool> Create(RequestConversation model)
    {
        try
        {

            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"INSERT INTO RequestConversations (Message,EmployeeObjectId,Rma) values({model.Message},{model.EmployeeObjectId},{model.Rma})");
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}