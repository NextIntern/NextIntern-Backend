using FluentValidation;

namespace SWD.NextIntern.Service.Services.CampaignService.GetById
{
    public class GetCampaignByIdValidation : AbstractValidator<GetCampaignByIdQuery>
    {
        public GetCampaignByIdValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id khong duoc de trong");
        }
    }
}
