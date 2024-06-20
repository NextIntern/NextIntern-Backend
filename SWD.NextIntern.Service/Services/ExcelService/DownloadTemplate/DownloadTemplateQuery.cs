using MediatR;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.Common.Interfaces;

namespace SWD.NextIntern.Service.Services.ExcelService.DownloadTemplate
{
    public class DownloadTemplateQuery : IRequest<FileContentResult>, IQuery
    {
    }
}
