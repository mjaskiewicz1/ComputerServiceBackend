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

public class RequestGetHistoryAll
{
    private readonly IRequestService _requestService;

    public RequestGetHistoryAll(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [FunctionName("RequestGetHistoryAll")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "requests/getHistoryAll")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<RequestDto>(requestBody);
            var model = await _requestService.GetHistoryAll(data.Rma);
            if (model == null) return new NotFoundResult();

            return new OkObjectResult(model);
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}