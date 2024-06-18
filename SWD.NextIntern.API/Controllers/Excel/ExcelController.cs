using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.Services.ExcelService.DownloadTemplate;

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

            if (result.Data is null)
            {
                return new BadRequestObjectResult("Something went wrong!");
            }

            return File(result.Data.FileContents, result.Data.ContentType, result.Data.FileDownloadName);
        }
    }
}
