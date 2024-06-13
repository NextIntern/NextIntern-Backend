using FluentValidation;

namespace SWD.NextIntern.Service.Services.CampaignService.Delete
{
    public class DeleteCampaignCommandValidation : AbstractValidator<DeleteCampaignCommand>
    {
        public DeleteCampaignCommandValidation()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Id can not be null");
        }
    }
}
