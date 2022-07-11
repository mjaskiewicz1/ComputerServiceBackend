using System;
using ComputerService.Backend;
using ComputerService.Backend.Interfaces;
using ComputerService.Backend.Services;
using Data.Context;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
[assembly: FunctionsStartup(typeof(Startup))]
namespace ComputerService.Backend;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var SqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
        builder.Services.AddDbContext<ComputerServiceContext>(
            options => options.UseSqlServer(SqlConnection));
        builder.Services.AddScoped<IInvoiceService, InvoiceService>();
        builder.Services.AddScoped<IRequestService, RequestService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IConfigService, ConfigService>();
        builder.Services.AddScoped<IServiceService, ServiceService>();
    }
}