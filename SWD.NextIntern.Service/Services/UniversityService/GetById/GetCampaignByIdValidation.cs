using FluentValidation;

namespace SWD.NextIntern.Service.Services.UniversityService.GetById
{
    public class GetUniversityByIdValidation : AbstractValidator<GetUniversityByIdQuery>
    {
        public GetUniversityByIdValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id is not null");
        }
    }
}
