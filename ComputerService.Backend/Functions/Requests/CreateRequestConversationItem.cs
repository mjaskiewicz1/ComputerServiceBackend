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

public class CreateRequestConversationItem
{
    private readonly IRequestService _service;

    public CreateRequestConversationItem(IRequestService service)
    {
        _service = service;
    }

    [FunctionName("CreateRequestConversationItem")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "requests/createMessageItem")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<RequestConversation>(requestBody);

            if (await _service.Create(data))
            {
                return new StatusCodeResult(201);
            }
            return new BadRequestResult();
        }

        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}