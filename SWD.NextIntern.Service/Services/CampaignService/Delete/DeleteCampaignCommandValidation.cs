using FluentValidation;

namespace SWD.NextIntern.Service.Services.CampaignService.Delete
{
    public class DeleteCampaignCommandValidation : AbstractValidator<DeleteCampaignCommand>
    {
        public DeleteCampaignCommandValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id khong duoc de trong");
        }
    }
}
