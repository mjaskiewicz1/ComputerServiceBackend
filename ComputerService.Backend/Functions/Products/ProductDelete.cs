using System;
using System.IO;
using System.Threading.Tasks;
using ComputerService.Backend.Dtos;
using ComputerService.Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ComputerService.Backend.Functions.Products;

public class ProductDelete
{
    private readonly IProductService _service;

    public ProductDelete(IProductService service)
    {
        _service = service;
    }

    [FunctionName("ProductDelete")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "products/delete")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var codeDto = JsonConvert.DeserializeObject<CodeDto>(requestBody);
            await _service.DeleteAsync(codeDto.Code);
            return new StatusCodeResult(200);
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}