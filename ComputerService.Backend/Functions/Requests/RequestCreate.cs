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

namespace ComputerService.Backend.Functions.Requests;

public class RequestCreate
{
    private readonly IRequestService _service;

    public RequestCreate(IRequestService service)
    {
        _service = service;
    }

    [FunctionName("RequestCreate")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "PUT", Route = "requests/create")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Request>(requestBody);
            Request model = await _service.CreateAsync(data);
            if (model != null)
            {
                var result = new ObjectResult(new { model.Rma, model.Url });
                result.StatusCode = 201;
                return result;
            }

            return new BadRequestResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}