
using FluentValidation;

namespace SWD.NextIntern.Service.Services.UniversityService.Delete
{
    public class DeleteUniversityCommandValidation : AbstractValidator<DeleteUniversityCommand>
    {
        public DeleteUniversityCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
