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

public class GetCreateInvoiceData
{
    private readonly IInvoiceService _invoice;

    public GetCreateInvoiceData(IInvoiceService invoice)
    {
        _invoice = invoice;
    }


    [FunctionName("GetCreateInvoiceData")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoices/getCreateInvoiceData")]
        HttpRequest req,
        ILogger log)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonConvert.DeserializeObject<RmaDto>(requestBody);
        var model = await _invoice.GetCreateInvoiceData(data.Rma);
        if (model == null) return new NotFoundResult();

        return new OkObjectResult(model);
    }
}