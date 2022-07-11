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

public class ProductUpdate
{
    private readonly IProductService _service;

    public ProductUpdate(IProductService service)
    {
        _service = service;
    }

    [FunctionName("ProductUpdate")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "products/update")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Product>(requestBody);
            if (await _service.ExistAsync(data.Model, data.Brand, data.Code))
                return new ConflictObjectResult("Produkt istnieje");
            var model = await _service.UpdateAsync(data);
            if (model != null)
                return new StatusCodeResult(200);
            return new BadRequestResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}