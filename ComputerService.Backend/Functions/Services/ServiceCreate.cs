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

namespace ComputerService.Backend.Functions.Services;

public class ServiceCreate
{
    private readonly IServiceService _service;

    public ServiceCreate(IServiceService service)
    {
        _service = service;
    }

    [FunctionName("ServiceCreate")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "services/create")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Service>(requestBody);

            if (await _service.ExistAsync(data.Name))
            {
                return new ConflictResult();
            }

            var model = await _service.CreateAsync(data);
            if (model != null) return new StatusCodeResult(201);
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }

        return new BadRequestResult();
    }
}