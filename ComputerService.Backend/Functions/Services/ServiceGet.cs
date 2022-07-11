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

namespace ComputerService.Backend.Functions.Services;

public class ServiceGet
{
    private readonly IServiceService _productService;

    public ServiceGet(IServiceService productService)
    {
        _productService = productService;
    }

    [FunctionName("ServiceGet")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route ="services/get")]
        HttpRequest req,
        ILogger log)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonConvert.DeserializeObject<CodeDto>(requestBody);
        var model = await _productService.GetAsync(data.Code);
        if (model == null) return new NotFoundResult();

        return new OkObjectResult(model);
    }
}