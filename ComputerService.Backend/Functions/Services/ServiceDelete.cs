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

public class ServiceDelete
{
    private readonly IServiceService _service;

    public ServiceDelete(IServiceService service)
    {
        _service = service;
    }

    [FunctionName("ServiceDelete")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "services/delete")]
        HttpRequest req,
        ILogger log)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var codeDto = JsonConvert.DeserializeObject<CodeDto>(requestBody);
        await _service.DeleteAsync(codeDto.Code);
        return new StatusCodeResult(200);
    }
}