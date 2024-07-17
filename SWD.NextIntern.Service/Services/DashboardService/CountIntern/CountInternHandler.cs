using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using SWD.NextIntern.Service.Services.DashboardService.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.DashboardService.CountIntern
{
    public class CountInternHandler : IRequestHandler<CountInternQuery, ResponseObject<List<CountInternDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly AppDbContext _dbContext;

        public CountInternHandler(IUserRepository userRepository, AppDbContext dbContext, IUniversityRepository universityRepository, ICampaignRepository campaignRepository)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _universityRepository = universityRepository;
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<List<CountInternDto>>> Handle(CountInternQuery request, CancellationToken cancellationToken)
        {
            List<CountInternDto> list = new List<CountInternDto>();

            var universityOptions = (IQueryable<University> query) =>
            {
                return query
                .Include(x => x.Campaigns)
                .Where(x => x.DeletedDate == null); ;
            };

            var universities = await _universityRepository.FindAllAsync(universityOptions, cancellationToken);


            foreach (var uni in universities)
            {
                var count = 0;
                var campaignOptions = (IQueryable<Campaign> query) =>
                {
                    return query
                    .Include(x => x.Users)
                    .Where(x => x.DeletedDate == null && x.UniversityId.Equals(uni.UniversityId));
                };

                var campaigns = await _campaignRepository.FindAllAsync(campaignOptions, cancellationToken);
                foreach (var c in campaigns)
                {
                    var userOptions = (IQueryable<User> query) =>
                    {
                        return query
                        .Include(x => x.Campaign)
                        .Where(x => x.DeletedDate == null && x.CampaignId.Equals(c.CampaignId));
                    };
                    var countUsers = await _userRepository.CountAsync(userOptions, cancellationToken);
                    count += countUsers;
                }

                list.Add(new CountInternDto
                {
                    University = uni.UniversityName,
                    Count = count
                });
            }

            return new ResponseObject<List<CountInternDto>>(list, HttpStatusCode.OK, "Success!");
        }
    }
}
