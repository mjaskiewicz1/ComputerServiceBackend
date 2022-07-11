using System;
using System.Threading.Tasks;
using ComputerService.Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace ComputerService.Backend.Functions.Products;

public class ProductGetAll
{
    private readonly IProductService _productService;

    public ProductGetAll(IProductService productService)
    {
        _productService = productService;
    }


    [FunctionName("ProductGetAll")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function,"get" , Route = "products/getAll")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var model = await _productService.GetAllAsync();
            return new OkObjectResult(model);
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}