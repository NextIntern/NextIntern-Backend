using FluentValidation;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.GetById
{
    public class GeFormCriteriaByIdValidation : AbstractValidator<GetFormCriteriaByIdQuery>
    {
        public GeFormCriteriaByIdValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id is not null");
        }
    }
}
