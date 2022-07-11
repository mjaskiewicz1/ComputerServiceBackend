using System.Linq;
using System.Threading.Tasks;
using ComputerService.Backend.Interfaces;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Backend.Services;

public class ConfigService : IConfigService
{
    private readonly ComputerServiceContext _context;

    public ConfigService(ComputerServiceContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync(Config model)
    {
        model.IsActive = true;
        var lastCompanyInformation = await _context.Configs.OrderBy(d => d.Id).LastOrDefaultAsync();
        if (lastCompanyInformation == null)
        {
            await _context.Configs.AddAsync(model);
        }
        else
        {
            lastCompanyInformation.IsActive = false;
            _context.Configs.Update(lastCompanyInformation);
            await _context.Configs.AddAsync(model);
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Config> GetActiveAsync()
    {
        return await _context.Configs.Select(company => new Config
        {
            Id = company.Id,
            Name = company.Name,
            Nip = company.Nip,
            Email = company.Email,
            PhoneNumber = company.PhoneNumber,
            City = company.City,
            Street = company.Street,
            Postcode = company.Postcode,
            IsActive = company.IsActive,
            BankAccountNumber = company.BankAccountNumber,
            PostalTown = company.PostalTown,
            BankName = company.BankName
        }).AsNoTracking().FirstOrDefaultAsync(c => c.IsActive == true);
    }

    public async Task<bool> ExistAsync()
    {
        return await _context.Configs.AnyAsync(c=>c.IsActive==true);
    }
}