using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComputerService.Backend.Dtos;
using Data.Models;

namespace ComputerService.Backend.Interfaces;

public interface IRequestService
{
    public Task<Request> CreateAsync(Request model);
    public Task<Request> GetAsync(string rma);
    public Task<RequestDto?> GetAsync(string rma, Guid url);
    public Task<Request> UpdateAsync(RequestDto model);
    public Task<IEnumerable<Request>> GetAllAsync();
    public Task<IEnumerable<RequestHistoryDto>> GetHistoryAll(string rma);
    public Task<bool> DeleteAsync(string rma);
    public Task<IEnumerable<Request>> FilterAsync(RequestFilterDto model);
    public Task<RequestDto?> GetRequestAndRequestConversation(string rma);
    public Task<bool> Create(RequestConversation model);
}