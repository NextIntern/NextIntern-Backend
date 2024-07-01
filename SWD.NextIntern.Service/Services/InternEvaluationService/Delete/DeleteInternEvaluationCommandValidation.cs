using FluentValidation;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.Delete
{
    public class DeleteInternEvaluationCommandValidation : AbstractValidator<DeleteInternEvaluationCommand>
    {
        public DeleteInternEvaluationCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
