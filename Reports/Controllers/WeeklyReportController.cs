using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.Core.Models;
using Reports.Core.Requests;
using Reports.Core.Requests.ReportRequests;
using Reports.Core.Responses.GoalResponses;
using Reports.Core.Responses.ReportResponses;
using Reports.Exceptions.ReportExceptions;
using Reports.Interfaces;

namespace Reports.Controllers
{
    public class WeeklyReportController : Controller
    {
        private readonly IRepository<WeeklyReport> _repository;
        private readonly IRepository<DailyReport> _dailyReportsRepository;
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IPagedFilter<WeeklyReport> _filter;
        private readonly IRepository<Goal> _goalsRepository;
        
        public WeeklyReportController(IRepository<WeeklyReport> repository, IRepository<Employee> employeesRepository
            ,IPagedFilter<WeeklyReport> filter,
            IRepository<Goal> goalsRepository,IRepository<DailyReport> dailyReportsRepository)
        {
            _repository = repository;
            _filter = filter;
            _goalsRepository = goalsRepository;
            _employeesRepository = employeesRepository;
            _dailyReportsRepository = dailyReportsRepository;
        }
        
        [HttpPost("/createWeeklyReport")]
        public async Task<CreateWeeklyReportResponse> CreateWeeklyReport([FromBody] CreateWeeklyReport createWeekly)
        {
            var weeklyReport = new WeeklyReport
            {
                OwnerId = createWeekly.OwnerId,
                Status = Status.Open,
                Content = createWeekly.Content,
                CreationTime = DateTime.UtcNow,
                LastChangeTime = DateTime.UtcNow
                
            };

            try
            {
                await _repository.CreateAsync(weeklyReport);

                return new CreateWeeklyReportResponse
                {
                    WeeklyReport = weeklyReport,
                    Success = true
                };
            }
            catch (CreateReportException e)
            {
                return new CreateWeeklyReportResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }
        
        [HttpPost("/updateWeeklyReport")]
        public async Task<UpdateWeeklyReportResponse> UpdateWeeklyReport([FromBody] UpdateWeeklyReport updateWeekly)
        {
            WeeklyReport weeklyReport = await _repository.GetByIdAsync(updateWeekly.Id);
            weeklyReport.Content = updateWeekly.Content;
            weeklyReport.Status = updateWeekly.Status;
            weeklyReport.LastChangeTime = DateTime.UtcNow;
            weeklyReport.Changes.Add(new Change
            {
                ChangerId = updateWeekly.ChangerId,
                Content = updateWeekly.Change,
                ChangeTime = DateTime.UtcNow
            });
            try
            {
                await _repository.UpdateAsync(weeklyReport);
                return new UpdateWeeklyReportResponse
                {
                    WeeklyReport = weeklyReport,
                    Success = true
                };
            }
            catch (UpdateReportException e)
            {
                return new UpdateWeeklyReportResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpPost("/addDailyReportInWeeklyReport")]
        public async Task<AddDailyReportInWeeklyReportResponse> AddDailyReport(
            [FromBody] AddDailyReportInWeeklyReport addDaily)
        {
            var weeklyReport = await _repository.GetByIdAsync(addDaily.Id);
            var dailyReport = await _dailyReportsRepository.GetByIdAsync(addDaily.DailyId);
            if (dailyReport.Status != Status.Resolved)
            {
                weeklyReport.DailyReports.Add(dailyReport);
                weeklyReport.Changes.Add(new Change
                {
                    ChangerId = addDaily.ChangerId,
                    Content = addDaily.Change,
                    ChangeTime = DateTime.UtcNow
                });
            }
            else
            {
                return new AddDailyReportInWeeklyReportResponse
                {
                    Success = false,
                    Error = "Daily Report is not ready!"
                };
            }

            weeklyReport.LastChangeTime = DateTime.UtcNow;
            try
            {
                await _repository.UpdateAsync(weeklyReport);
                return new AddDailyReportInWeeklyReportResponse()
                {
                    WeeklyReport = weeklyReport,
                    Success = true
                };
            }
            catch (UpdateReportException e)
            {
                return new AddDailyReportInWeeklyReportResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpGet("/getByIdWeeklyReport/{id:int}")]
        public async Task<GetByIdWeeklyReportResponse> GetByIdWeeklyReport([FromRoute] int id)
        {
            try
            {
                var report = await _repository.GetByIdAsync(id);
                return new GetByIdWeeklyReportResponse
                {
                    WeeklyReport = report,
                    Success = true
                };
            }
            catch (GetByIdReportException e)
            {
                return new GetByIdWeeklyReportResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpGet("/getGoalsInWeeklyReport/{id:int}")]
        public async Task<GetGoalsInWeeklyReportResponse> GetGoalsInWeeklyReport([FromRoute] int id)
        {
            var weeklyReport = await _repository.GetByIdAsync(id);
            if (weeklyReport == null)
                return new GetGoalsInWeeklyReportResponse
                {
                    Success = false,
                    Error = "Employee not found"
                };
            var goals = weeklyReport.DailyReports.SelectMany(d => d.Goals).ToList();
            return new GetGoalsInWeeklyReportResponse
            {
                Success = true,
                Goals = goals
            };
        }
    }
}