using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerService.Backend.Interfaces;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Backend.Services;

public class ServiceService : IServiceService
{
    private readonly ComputerServiceContext _context;

    public ServiceService(ComputerServiceContext context)
    {
        _context = context;
    }

    private string ServiceCode
    {
        get
        {
            var generator = new Random();
            var r = generator.Next(0, 999999).ToString("D11");
            return $"S{r}";
        }
    }

    public async Task<Service> CreateAsync(Service model)
    {
        model.Code = ServiceCode;
        await _context.Services.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<bool> ExistAsync(string name)
    {
        return await _context.Services.AnyAsync(s => s.Name == name);
    }

    public async Task<bool> ExistAsync(string name, string ignoreCode)
    {
        return await _context.Services.AnyAsync(s => s.Name == name && s.Code != ignoreCode);
    }

    public async Task DeleteAsync(string code)
    {
        var model = await _context.Services.FindAsync(code);
        if (model != null)
        {
            _context.Services.Remove(model);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Service> UpdateAsync(Service model)
    {
        _context.Services.Update(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<Service> GetAsync(string code)
    {
        return await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Code == code);
    }

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        return await _context.Services.Select(s => new Service
        {
            Code = s.Code,
            Name = s.Name,
            Price = s.Price,
            Tax = s.Tax,
            BasicUnit = s.BasicUnit,
            CreatedDateTime =s.CreatedDateTime
        }).AsNoTracking().OrderByDescending(p => p.CreatedDateTime).ToListAsync();
    }
}