using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.Core.Models;
using Reports.Core.Requests;
using Reports.Core.Requests.ReportRequests;
using Reports.Core.Responses;
using Reports.Core.Responses.ReportResponses;
using Reports.Exceptions.ReportExceptions;
using Reports.Interfaces;
using Reports.Services.Repositories.Employees;

namespace Reports.Controllers
{
    public class DailyReportController : Controller
    {
        private readonly IRepository<DailyReport> _repository;
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IPagedFilter<DailyReport> _filter;
        private readonly IRepository<Goal> _goalsRepository;
        
        public DailyReportController(IRepository<DailyReport> repository, IRepository<Employee> employeesRepository
            , IPagedFilter<DailyReport> filter,
            IRepository<Goal> goalsRepository)
        {
            _repository = repository;
            _filter = filter;
            _goalsRepository = goalsRepository;
            _employeesRepository = employeesRepository;
        }

        [HttpPost("/createDailyReport")]
        public async Task<CreateDailyReportResponse> CreateDailyReport([FromBody] CreateDailyReport createDaily)
        {
            var dailyReport = new DailyReport
            {
                OwnerId = createDaily.OwnerId,
                Status = Status.Open,
                Content = createDaily.Content,
                CreationTime = DateTime.UtcNow,
                LastChangeTime = DateTime.UtcNow
                
            };
            try
            {
                await _repository.CreateAsync(dailyReport);

                return new CreateDailyReportResponse
                {
                    DailyReport = dailyReport,
                    Success = true
                };
            }
            catch (CreateReportException e)
            {
                return new CreateDailyReportResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpPost("/updateDailyReport")]
        public async Task<UpdateDailyReportResponse> UpdateDailyReport([FromBody] UpdateDailyReport updateDaily)
        {
            DailyReport dailyReport = await _repository.GetByIdAsync(updateDaily.Id);
            dailyReport.Content = updateDaily.Content;
            dailyReport.Status = updateDaily.Status;
            dailyReport.LastChangeTime = DateTime.UtcNow;
            dailyReport.Changes.Add(new Change
            {
                Content = updateDaily.Content,
                ChangerId = updateDaily.ChangerId,
                ChangeTime = DateTime.UtcNow
            });
            try
            {
                await _repository.UpdateAsync(dailyReport);
                return new UpdateDailyReportResponse
                {
                    DailyReport = dailyReport,
                    Success = true
                };
            }
            catch (UpdateReportException e)
            {
                return new UpdateDailyReportResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }
        
        [HttpPost("/addGoalInDailyReport")]
        public async Task<AddGoalInDailyReportResponse> AddGoalInDailyReport([FromBody] AddGoalInDailyReport addGoal)
        {
            var goal = await _goalsRepository.GetByIdAsync(addGoal.GoalId);
            var dailyReport = await _repository.GetByIdAsync(addGoal.Id);
            dailyReport.Goals.Add(goal);
            dailyReport.Changes.Add(new Change
            {
                ChangerId = addGoal.ChangerId,
                Content = addGoal.Change,
                ChangeTime = DateTime.UtcNow
            });
            dailyReport.LastChangeTime = DateTime.UtcNow;
            try
            {
                await _repository.UpdateAsync(dailyReport);
                return new AddGoalInDailyReportResponse
                {
                    DailyReport = dailyReport,
                    Success = true
                };
            }
            catch (UpdateReportException e)
            {
                return new AddGoalInDailyReportResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpGet("/getByIdDailyReport/{id:int}")]
        public async Task<GetByIdDailyReportResponse> GetByIdDailyReport([FromRoute] int id)
        {
            try
            {
                var report = await _repository.GetByIdAsync(id);
                return new GetByIdDailyReportResponse
                {
                    DailyReport = report,
                    Success = true
                };
            }
            catch (GetByIdReportException e)
            {
                return new GetByIdDailyReportResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }
        
        [HttpPost("/getSlavesDailyReportsGoals")]
        public async Task<GetDailyReportsResponse> GetSlavesDailyAsync([FromBody] GetSlavesDailyReports dailyReports)
        {
            Employee employee = await _employeesRepository.GetByIdAsync(dailyReports.OwnerId);
            if (employee == null)
                return new GetDailyReportsResponse()
                {
                    Success = false,
                    Error = "Employee not found"
                };
            if (dailyReports.Readiness == true)
            {
                var reports = employee.Slaves.SelectMany(slave => slave.DailyReports)
                    .Where(d => d.Status == Status.Resolved).ToList();
                return new GetDailyReportsResponse()
                {
                    Success = true,
                    DailyReports = reports
                };
            }
            else
            {
                var reports = employee.Slaves.SelectMany(slave => slave.DailyReports)
                    .Where(d => d.Status == Status.Active || d.Status == Status.Open).ToList();
                return new GetDailyReportsResponse()
                {
                    Success = true,
                    DailyReports = reports
                };
            }

        }
    }
}