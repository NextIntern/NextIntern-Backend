
using FluentValidation;

namespace SWD.NextIntern.Service.Services.InternService.Delete
{
    public class DeleteInternCommandValidation : AbstractValidator<DeleteInternCommand>
    {
        public DeleteInternCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
