using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.DashboardService._5MostIntern;
using SWD.NextIntern.Service.Services.DashboardService.CountIntern;
using System.Net;

namespace SWD.NextIntern.Service.Services.DashboardService.MostIntern
{
    public class MostInternHandler : IRequestHandler<MostInternQuery, ResponseObject<List<MostInternDto>>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IUniversityRepository _universityRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICampaignRepository _campaignRepository;

        public MostInternHandler(AppDbContext dbContext, IUniversityRepository universityRepository, IUserRepository userRepository, ICampaignRepository campaignRepository)
        {
            _dbContext = dbContext;
            _universityRepository = universityRepository;
            _userRepository = userRepository;
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<List<MostInternDto>>> Handle(MostInternQuery request, CancellationToken cancellationToken)
        {
            List<MostInternDto> list = new List<MostInternDto>();

            var top5List = await _dbContext.InternEvaluationCriteria
                .Include(s => s.InternEvaluation)
                .Where(s => s.DeletedDate == null)
                .OrderByDescending(s => s.Score)
                .Take(5)
                .ToListAsync();

            foreach (var ie in top5List)
            {
                var internOptions = (IQueryable<User> query) =>
                {
                    return query
                    .Include(x => x.Campaign)
                    .Where(x => x.DeletedDate == null && ie.InternEvaluation.InternId.Equals(x.UserId)); ;
                };

                var user = await _userRepository.FindAsync(internOptions, cancellationToken);

                list.Add(new MostInternDto
                {
                    InternId = user.UserId,
                    Name = user.FullName,
                    Score = ie.Score
                });
            }

            return new ResponseObject<List<MostInternDto>>(list, HttpStatusCode.OK, "Success!");
        }
    }
}
