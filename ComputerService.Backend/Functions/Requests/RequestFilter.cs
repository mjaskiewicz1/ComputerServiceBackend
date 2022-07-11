using System;
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

namespace ComputerService.Backend.Functions.Requests;

public class RequestFilter
{
    private readonly IRequestService _requestService;

    public RequestFilter(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [FunctionName("RequestFilter")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "requests/filter")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<RequestFilterDto>(requestBody);
            var model = await _requestService.FilterAsync(data);
            if (model == null) return new NotFoundResult();

            return new OkObjectResult(model);
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}