using FluentValidation;
using SWD.NextIntern.Service.Services.InternService.GetById;

namespace SWD.NextIntern.Service.Services.InternEvaluationCriteriaService.GetById
{
    public class GetInternEvaluationCriteriaByIdValidation : AbstractValidator<GetInternEvaluationCriteriaByIdQuery>
    {
        public GetInternEvaluationCriteriaByIdValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id is not null");
        }
    }
}
