
using FluentValidation;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.Delete
{
    public class DeleteEvaluationFormCommandValidation : AbstractValidator<DeleteEvaluationFormCommand>
    {
        public DeleteEvaluationFormCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
