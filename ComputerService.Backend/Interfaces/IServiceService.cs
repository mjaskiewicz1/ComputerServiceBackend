using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;

namespace ComputerService.Backend.Interfaces;

public interface IServiceService
{
    public Task<Service> CreateAsync(Service model);
    public Task<bool> ExistAsync(string name);
    public Task<bool> ExistAsync(string name, string ignoreCode);
    public Task DeleteAsync(string code);
    public Task<Service> UpdateAsync(Service model);
    public Task<Service> GetAsync(string code);
    public Task<IEnumerable<Service>> GetAllAsync();
}