using System;
using System.IO;
using System.Threading.Tasks;
using ComputerService.Backend.Dtos;
using ComputerService.Backend.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ComputerService.Backend.Functions.Requests;

public class RequestUpdate
{
    private readonly IRequestService _service;

    public RequestUpdate(IRequestService service)
    {
        _service = service;
    }

    [FunctionName("RequestUpdate")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "requests/update")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<RequestDto>(requestBody);
            Request model = await _service.UpdateAsync(data);
            if (model != null) return new OkResult();

            return new BadRequestResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}