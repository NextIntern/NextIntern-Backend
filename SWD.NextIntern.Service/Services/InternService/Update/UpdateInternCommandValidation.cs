
using FluentValidation;

namespace SWD.NextIntern.Service.Services.InternService.Update
{
    public class UpdateInternCommandValidation : AbstractValidator<UpdateInternCommand>
    {
        public UpdateInternCommandValidation()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(BeAValidGuid).WithMessage("Id must be a valid GUID.");

            RuleFor(command => command.CampaignId)
                .Must(BeAValidGuidOrEmpty).WithMessage("CampaignId must be a valid GUID.");

            RuleFor(command => command.Telephone)
                .NotEmpty().WithMessage("Phone is required.")
                .MinimumLength(10).WithMessage("Phone must not at least 10 characters.")
                .MaximumLength(11).WithMessage("Phone must not exceed 11 characters.");

            RuleFor(command => command.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(100).WithMessage("Address must not exceed 100 characters.");

            RuleFor(command => command.Fullname)
                .NotEmpty().WithMessage("Fullname is required.")
                .MaximumLength(100).WithMessage("Fullname must not exceed 100 characters.");

            RuleFor(command => command.Gender)
                .Must(BeAValidGender).WithMessage("Gender must be either male or female.");

            RuleFor(command => command.Dob)
                .NotNull().WithMessage("Date of Birth is required.")
                .Must(BeAValidDate).WithMessage("Date of Birth cannot be in the future.")
                .Must(BeWithinValidRange).WithMessage("Date of Birth must be within the last 100 years.")
                .Must(NotBeTooOld).WithMessage("Date of Birth cannot be more than 100 years ago.");

        }

        private bool BeAValidGuid(string id)
        {
            return Guid.TryParse(id, out _);
        }

        private bool BeAValidGuidOrEmpty(string? id)
        {
            return string.IsNullOrEmpty(id) || Guid.TryParse(id, out _);
        }

        private bool BeAValidGender(string gender)
        {
            return gender.Equals("Male", StringComparison.OrdinalIgnoreCase) || gender.Equals("Female", StringComparison.OrdinalIgnoreCase);
        }

        private bool BeAValidDate(DateOnly? dob)
        {
            if (!dob.HasValue)
            {
                return false;
            }

            return dob.Value <= DateOnly.FromDateTime(DateTime.Today);
        }

        private bool BeWithinValidRange(DateOnly? dob)
        {
            if (!dob.HasValue)
            {
                return false;
            }

            var hundredYearsAgo = DateOnly.FromDateTime(DateTime.Today.AddYears(-100));
            return dob.Value >= hundredYearsAgo;
        }

        private bool NotBeTooOld(DateOnly? dob)
        {
            if (!dob.HasValue)
            {
                return false;
            }

            var hundredYearsAgo = DateOnly.FromDateTime(DateTime.Today.AddYears(-100));
            return dob.Value > hundredYearsAgo;
        }
    }
}
