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

public class RequestDelete
{
    private readonly IRequestService _requestService;

    public RequestDelete(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [FunctionName("RequestDelete")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "requests/delete")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<RequestDto>(requestBody);
            var model = await _requestService.DeleteAsync(data.Rma);
            if (model == false) return new NotFoundResult();

            return new OkResult();
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}