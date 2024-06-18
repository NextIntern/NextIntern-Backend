using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.ExcelService.DownloadTemplate
{
    public class DownloadTemplateQuery : IRequest<ResponseObject<FileContentResult?>>, IQuery
    {
    }
}
