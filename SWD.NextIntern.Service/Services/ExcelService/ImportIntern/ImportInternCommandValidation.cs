using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace SWD.NextIntern.Service.Services.ExcelService.ImportIntern
{
    public class ImportInternCommandValidation : AbstractValidator<ImportInternCommand>
    {
        public ImportInternCommandValidation()
        {
            RuleFor(x => x.File)
                .NotEmpty().WithMessage("File is required.")
                .Must(BeAValidExcelFile).WithMessage("File must be a valid Excel file.");
        }

        private bool BeAValidExcelFile(IFormFile file)
        {
            // Example validation: Check if the file is an Excel file
            // You can add more complex validation logic here if needed
            if (file == null)
                return false;

            var allowedExtensions = new[] { ".xlsx", ".xls" };
            var fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(fileExtension);
        }
    }
}
