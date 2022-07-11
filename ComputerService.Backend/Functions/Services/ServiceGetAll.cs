using System.Threading.Tasks;
using ComputerService.Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace ComputerService.Backend.Functions.Services;

public class ServiceGetAll
{
    private readonly IServiceService _service;

    public ServiceGetAll(IServiceService service)
    {
        _service = service;
    }


    [FunctionName("ServiceGetAll")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "services/getAll")]
        HttpRequest req,
        ILogger log)
    {
        var model = await _service.GetAllAsync();
        if (model == null)
            return new NotFoundResult();
        return new OkObjectResult(model);
    }
}