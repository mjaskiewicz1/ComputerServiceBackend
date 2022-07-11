using System.Threading.Tasks;
using Data.Models;

namespace ComputerService.Backend.Interfaces;

public interface IConfigService
{
    public Task<bool> CreateAsync(Config model);
    public Task<Config> GetActiveAsync();
    public Task<bool> ExistAsync();
}