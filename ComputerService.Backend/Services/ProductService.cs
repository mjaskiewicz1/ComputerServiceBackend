using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerService.Backend.Interfaces;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Backend.Services;

public class ProductService : IProductService
{
    private readonly ComputerServiceContext _context;

    public ProductService(ComputerServiceContext context)
    {
        _context = context;
    }

    public string ProductNumber
    {
        get
        {
            var generator = new Random();
            var r = generator.Next(0, 999999).ToString("D11");
            return $"P{r}";
        }
    }


    public async Task<Product> CreateAsync(Product model)
    {
        model.Code = ProductNumber;
        await _context.Products.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<Product> UpdateAsync(Product model)
    {
        _context.Products.Update(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<Product> GetAsync(string code)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Code == code);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.AsNoTracking().OrderByDescending(p => p.CreatedDateTime).ToListAsync();
    }

    public async Task DeleteAsync(string code)
    {
        var model = await _context.Products.FindAsync(code);

        if (model != null)
        {
            _context.Products.Remove(model);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistAsync(string model, string brand)
    {
        return await _context.Products.AnyAsync(p => p.Model == model && p.Brand == brand);
    }

    public async Task<bool> ExistAsync(string model, string brand, string ignoreCode)
    {
        return await _context.Products.AnyAsync(p => p.Model == model && p.Brand == brand && p.Code != ignoreCode);
    }
}