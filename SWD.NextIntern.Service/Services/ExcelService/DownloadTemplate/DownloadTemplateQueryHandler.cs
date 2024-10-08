﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.ExcelService.DownloadTemplate
{
    public class DownloadTemplateQueryHandler : IRequestHandler<DownloadTemplateQuery, FileContentResult>
    {
        public async Task<FileContentResult> Handle(DownloadTemplateQuery request, CancellationToken cancellationToken)
        {

            // Ensure EPPlus license context is set
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                // Add a worksheet to the Excel package
                var worksheet = package.Workbook.Worksheets.Add("Intern Template");

                // Add headers
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "FullName";
                worksheet.Cells[1, 3].Value = "Dob";
                worksheet.Cells[1, 4].Value = "Gender";
                worksheet.Cells[1, 5].Value = "Telephone";
                worksheet.Cells[1, 6].Value = "Email";
                worksheet.Cells[1, 7].Value = "Address";

                // Convert the Excel package to a byte array
                var bytes = package.GetAsByteArray();

                // Define the content type for Excel files
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "InternTemplate.xlsx";

                // Return the file content result
                var result = new FileContentResult(bytes, contentType) { FileDownloadName = fileName };
                return await Task.FromResult(result);
            }
        }
    }
}
