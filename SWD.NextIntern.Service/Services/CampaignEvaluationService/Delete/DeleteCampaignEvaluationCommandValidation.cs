using FluentValidation;

namespace SWD.NextIntern.Service.Services.CampaignEvaluationService.Delete
{
    public class DeleteCampaignEvaluationCommandValidation : AbstractValidator<DeleteCampaignEvaluationCommand>
    {
        public DeleteCampaignEvaluationCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
