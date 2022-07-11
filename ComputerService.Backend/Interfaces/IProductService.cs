using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;

namespace ComputerService.Backend.Interfaces;

public interface IProductService
{
    public Task<Product> CreateAsync(Product model);
    public Task<Product> UpdateAsync(Product model);
    public Task<Product> GetAsync(string code);
    public Task<IEnumerable<Product>> GetAllAsync();
    public Task DeleteAsync(string code);
    public Task<bool> ExistAsync(string model, string brand);
    public Task<bool> ExistAsync(string model, string brand, string ignoreCode);
}