
using FluentValidation;

namespace SWD.NextIntern.Service.Services.FormCriteriaService.Delete
{
    public class DeleteFormCriteriaCommandValidation : AbstractValidator<DeleteFormCriteriaCommand>
    {
        public DeleteFormCriteriaCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
