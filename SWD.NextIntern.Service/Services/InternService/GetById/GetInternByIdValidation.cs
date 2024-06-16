using FluentValidation;
using SWD.NextIntern.Service.Services.InternService.GetById;

namespace SWD.NextIntern.Service.Services.InternService.GetById
{
    public class GetInternByIdValidation : AbstractValidator<GetInternByIdQuery>
    {
        public GetInternByIdValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id is not null");
        }
    }
}
