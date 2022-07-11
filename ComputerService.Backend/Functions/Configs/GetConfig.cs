using System;
using System.Threading.Tasks;
using ComputerService.Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace ComputerService.Backend.Functions.Configs;

public class GetConfig
{
    private readonly IConfigService _configService;

    public GetConfig(IConfigService configService)
    {
        _configService = configService;
    }

    [FunctionName("GetConfig")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "configs/get")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var model = await _configService.GetActiveAsync();
            if (model == null) return new NotFoundResult();

            return new OkObjectResult(model);
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}