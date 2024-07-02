using FluentValidation;
using SWD.NextIntern.Service.Services.InternEvaluationService.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.UniversityService.Create
{
    public class CreateUniversityCommandValidation : AbstractValidator<CreateUniversityCommand>
    {
        public CreateUniversityCommandValidation()
        {
            RuleFor(command => command.UniversityName)
                .NotEmpty().WithMessage("UniversityName is required.")
                .MaximumLength(100).WithMessage("UniversityName must not exceed 100 characters.");

            RuleFor(command => command.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(100).WithMessage("Address must not exceed 100 characters.");

            RuleFor(command => command.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MinimumLength(10).WithMessage("Phone must not at least 10 characters.")
                .MaximumLength(11).WithMessage("Phone must not exceed 11 characters.");
        }

        private bool BeAValidGuid(string id)
        {
            return Guid.TryParse(id, out _);
        }

        private bool BeAValidGuidOrEmpty(string? id)
        {
            return string.IsNullOrEmpty(id) || Guid.TryParse(id, out _);
        }
    }
}
