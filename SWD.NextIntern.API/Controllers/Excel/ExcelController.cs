using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.ExcelService.DownloadTemplate;
using SWD.NextIntern.Service.Services.ExcelService.ImportIntern;

namespace SWD.NextIntern.API.Controllers.Excel
{
    [Route("api/v1/excel")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExcelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("download-template")]
        public async Task<IActionResult> DownloadExcel(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DownloadTemplateQuery(), cancellationToken);

            if (result is null)
            {
                return new BadRequestObjectResult("Something went wrong!");
            }

            return File(result.FileContents, result.ContentType, result.FileDownloadName);
        }

        [HttpPost("import-intern")]
        public async Task<ResponseObject<string>> ImportFromExcel([FromForm] ImportInternCommand command, CancellationToken cancellationToken = default)
        {
            //if (file == null || file.Length == 0)
            //{
            //    return new ResponseObject<string>(System.Net.HttpStatusCode.BadRequest, "No file uploaded.");
            //}

            //var command = new ImportInternCommand { File = file };
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }
    }
}
