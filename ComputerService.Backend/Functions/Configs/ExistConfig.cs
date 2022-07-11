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

namespace ComputerService.Backend.Functions.Configs;

public class ExistConfig
{
    private readonly IConfigService _configService;

    public ExistConfig(IConfigService configService)
    {
        _configService = configService;
    }

    [FunctionName("ExistConfig")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "configs/exist")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var model = await _configService.ExistAsync();
            if (model)
            {
                return new OkResult();
            }
            else
            {
                return new NotFoundResult();
               
            }

           
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}