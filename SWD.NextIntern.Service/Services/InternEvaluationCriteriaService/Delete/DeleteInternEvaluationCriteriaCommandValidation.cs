
using FluentValidation;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.Delete
{
    public class DeleteInternEvaluationCriteriaCommandValidation : AbstractValidator<DeleteInternEvaluationCriteriaCommand>
    {
        public DeleteInternEvaluationCriteriaCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
