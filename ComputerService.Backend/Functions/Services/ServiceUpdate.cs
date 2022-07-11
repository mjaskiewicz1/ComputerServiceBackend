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

public class ServiceUpdate
{
    private readonly IServiceService _service;

    public ServiceUpdate(IServiceService service)
    {
        _service = service;
    }

    [FunctionName("ServiceUpdate")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "services/update")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Service>(requestBody);

            if (!await _service.ExistAsync(data.Name)) return new NotFoundResult();

            if (await _service.ExistAsync(data.Name, data.Code)) return new ConflictResult();

            var model = await _service.UpdateAsync(data);
            if (model != null) return new StatusCodeResult(200);
            return new BadRequestResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}