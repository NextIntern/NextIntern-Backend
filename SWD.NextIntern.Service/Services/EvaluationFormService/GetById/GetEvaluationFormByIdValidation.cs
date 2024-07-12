using FluentValidation;

namespace SWD.NextIntern.Service.Services.EvaluationFormService.GetById
{
    public class GetEvaluationFormByIdValidation : AbstractValidator<GetEvaluationFormByIdQuery>
    {
        public GetEvaluationFormByIdValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id is not null");
        }
    }
}
