using FluentValidation;

namespace SWD.NextIntern.Service.Services.UniversityService.GetById
{
    public class GetUniversityByIdQueryValidation : AbstractValidator<GetUniversityByIdQuery>
    {
        public GetUniversityByIdQueryValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
