using FluentValidation;

namespace SWD.NextIntern.Service.Services.InternEvaluationService.GetInternEvaluationById
{
    public class GetInternEvaluationByIdQueryValidation : AbstractValidator<GetInternEvaluationByIdQuery>
    {
        public GetInternEvaluationByIdQueryValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
