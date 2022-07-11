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

namespace ComputerService.Backend.Functions.Invoices;

public class GetInvoice
{
    private readonly IInvoiceService _service;

    public GetInvoice(IInvoiceService service)
    {
        _service = service;
    }


    [FunctionName("GetInvoice")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoices/get")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<InvoiceNumberDto>(requestBody);
            var invoice = await _service.Get(data.Number);
            if (invoice == null) return new NotFoundResult();

            return new OkObjectResult(invoice);
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}