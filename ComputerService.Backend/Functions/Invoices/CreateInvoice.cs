using System;
using System.IO;
using System.Threading.Tasks;
using ComputerService.Backend.Dtos;
using ComputerService.Backend.Interfaces;
using ComputerService.Backend.Models.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ComputerService.Backend.Functions.Invoices;

public class CreateInvoice
{
    private readonly IInvoiceService _service;

    public CreateInvoice(IInvoiceService service)
    {
        _service = service;
    }

    [FunctionName("CreateInvoice")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "invoices/create")]
        HttpRequest req,
        ILogger log)

    {
        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<InvoiceDto>(requestBody);
            var model = await _service.Create(data);
            return new ObjectResult(new InvoiceNumberDto(model.Number)) { StatusCode = StatusCodes.Status201Created };
        }
        catch (ConflictException e)
        {
            return new ConflictObjectResult(e.Message);
        }
        catch (Exception e)
        {
            return new BadRequestResult();
        }
    }
}