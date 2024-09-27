using MediatR;
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWD.NextIntern.Service.Services.DashboardService.Items
{
    public class ItemsHandler : IRequestHandler<ItemsQuery, ResponseObject<List<ItemsDto>>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IUniversityRepository _universityRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IEvaluationFormRepository _evaluationFormRepository;
        private readonly string universityTitle = "Total University";
        private readonly string userTitle = "Total Internship";
        private readonly string campaignTitle = "Total Campaign";
        private readonly string formTitle = "Total Evaluation Form";

        public ItemsHandler(IUniversityRepository universityRepository, IUserRepository userRepository, ICampaignRepository campaignRepository, IEvaluationFormRepository evaluationFormRepository)
        {
            _universityRepository = universityRepository;
            _userRepository = userRepository;
            _campaignRepository = campaignRepository;
            _evaluationFormRepository = evaluationFormRepository;
        }

        public async Task<ResponseObject<List<ItemsDto>>> Handle(ItemsQuery request, CancellationToken cancellationToken)
        {
            List<ItemsDto> list = new List<ItemsDto>();

            var now = DateTime.Now;
            var firstDayOfCurrentMonth = new DateTime(now.Year, now.Month, 1);
            var firstDayOfLastMonth = firstDayOfCurrentMonth.AddMonths(-1);

            var currentMonthStats = await GetMonthlyStatisticsAsync(firstDayOfCurrentMonth);
            var lastMonthStats = await GetMonthlyStatisticsAsync(firstDayOfLastMonth);    

            list.Add(new ItemsDto
            {
                Title = universityTitle,
                Total = currentMonthStats.TotalUniversity,
                Percentage = CalculatePercentageChange(lastMonthStats.TotalUniversity, currentMonthStats.TotalUniversity),
                IsIncrease = CalculatePercentageChange(lastMonthStats.TotalUniversity, currentMonthStats.TotalUniversity) > 0
            });

            list.Add(new ItemsDto
            {
                Title = userTitle,
                Total = currentMonthStats.TotalUser,
                Percentage = CalculatePercentageChange(lastMonthStats.TotalUser, currentMonthStats.TotalUser),
                IsIncrease = CalculatePercentageChange(lastMonthStats.TotalUser, currentMonthStats.TotalUser) > 0
            });
            list.Add(new ItemsDto
            {
                Title = campaignTitle,
                Total = currentMonthStats.TotalCampaign,
                Percentage = CalculatePercentageChange(lastMonthStats.TotalCampaign, currentMonthStats.TotalCampaign),
                IsIncrease = CalculatePercentageChange(lastMonthStats.TotalCampaign, currentMonthStats.TotalCampaign) > 0
            });

            list.Add(new ItemsDto
            {
                Title = formTitle,
                Total = currentMonthStats.TotalForm,
                Percentage = CalculatePercentageChange(lastMonthStats.TotalForm, currentMonthStats.TotalForm),
                IsIncrease = CalculatePercentageChange(lastMonthStats.TotalForm, currentMonthStats.TotalForm) > 0
            });

            return new ResponseObject<List<ItemsDto>>(list, HttpStatusCode.OK, "Success!");
        }

        private async Task<MonthlyStatistics> GetMonthlyStatisticsAsync(DateTime startDate)
        {
            var endDate = startDate.AddMonths(1);

            return new MonthlyStatistics
            {
                TotalUniversity = await _universityRepository.CountAsync(u => u.DeletedDate == null && u.CreateDate >= startDate && u.CreateDate < endDate),
                TotalUser = await _userRepository.CountAsync(u => u.DeletedDate == null && u.CreateDate >= startDate && u.CreateDate < endDate),
                TotalCampaign = await _campaignRepository.CountAsync(c => c.DeletedDate == null && c.CreateDate >= startDate && c.CreateDate < endDate),
                TotalForm = await _evaluationFormRepository.CountAsync(f => f.DeletedDate == null && f.CreateDate >= startDate && f.CreateDate < endDate)
            };
        }

        private decimal CalculatePercentageChange(int oldValue, int newValue)
        {
            if (oldValue == 0)
                return newValue > 0 ? 100 : 0;

            return Math.Round(((decimal)(newValue - oldValue) / oldValue) * 100, 2);
        }

        public class MonthlyStatistics
        {
            public int TotalUniversity { get; set; }
            public int TotalUser { get; set; }
            public int TotalCampaign { get; set; }
            public int TotalForm { get; set; }
        }
    }
}
