using System;
using System.Threading.Tasks;
using ComputerService.Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace ComputerService.Backend.Functions.Requests;

public class RequestGetAll
{
    private readonly IRequestService _requestService;

    public RequestGetAll(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [FunctionName("RequestGetAll")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "requests/getAll")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var model = await _requestService.GetAllAsync();
            if (model == null)
                return new NotFoundResult();
            return new OkObjectResult(model);
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}