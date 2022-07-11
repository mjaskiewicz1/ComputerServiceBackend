using System.Threading.Tasks;
using ComputerService.Backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace ComputerService.Backend.Functions.Invoices;

public class GetAllInvoice
{
    private readonly IInvoiceService _service;

    public GetAllInvoice(IInvoiceService service)
    {
        _service = service;
    }

    [FunctionName("GetAllInvoice")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "invoices/getAll")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var model = await _service.GetAll();
            if (model == null)
                return new NotFoundResult();
            return new OkObjectResult(model);
        }
        catch
        {
            return new BadRequestResult();
        }
    }
}