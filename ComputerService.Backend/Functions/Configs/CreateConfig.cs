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

public class CreateConfig
{
    private readonly IConfigService _configService;

    public CreateConfig(IConfigService configService)
    {
        _configService = configService;
    }

    [FunctionName("CreateConfig")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "configs/create")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Config>(requestBody);
            if (await _configService.CreateAsync(data))
                return new StatusCodeResult(201);
            return new BadRequestResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}