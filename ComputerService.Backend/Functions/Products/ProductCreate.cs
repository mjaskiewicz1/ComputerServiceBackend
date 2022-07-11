using System;
using System.IO;
using System.Threading.Tasks;
using ComputerService.Backend.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ComputerService.Backend.Functions.Products;

public class ProductCreate
{
    private readonly IProductService _service;

    public ProductCreate(IProductService service)
    {
        _service = service;
    }

    [FunctionName("ProductCreate")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "products/create")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Product>(requestBody);
            if (await _service.ExistAsync(data.Model, data.Brand)) return new ConflictObjectResult("Produkt istnieje");
            var model = await _service.CreateAsync(data);
            return model != null ? new StatusCodeResult(201) : new BadRequestResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}