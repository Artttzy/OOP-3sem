using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.Core.Models;
using Reports.Core.Requests;
using Reports.Core.Requests.GoalRequests;
using Reports.Core.Responses.GoalResponses;
using Reports.Exceptions.GoalExceptions;
using Reports.Interfaces;
using Reports.Services.Repositories.Employees;

namespace Reports.Controllers
{
    public class GoalController : Controller
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Goal> _repository;
        private readonly IPagedFilter<Goal> _filter;
        
        public GoalController(IRepository<Employee> employeeRepository, IRepository<Goal> repository, IPagedFilter<Goal> filter)
        {
            _employeeRepository = employeeRepository;
            _repository = repository;
            _filter = filter;
        }

        [HttpPost("/createGoal")]
        public async Task<CreateGoalResponse> CreateGoal([FromBody] CreateGoal createGoal)
        {
            var goal = new Goal
            {
                CreationTime = DateTime.UtcNow,
                LastChangeTime = DateTime.UtcNow,
                OwnerId = createGoal.OwnerId,
                Status = Status.Open
            };
            try
            {
                await _repository.CreateAsync(goal);
                return new CreateGoalResponse
                {
                    Goal = goal,
                    Success = true
                };
            }
            catch (CreateGoalException e)
            {
                return new CreateGoalResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpPost("/updateGoal")]
        public async Task<UpdateGoalResponse> UpdateGoal([FromBody] UpdateGoal updateGoal)
        {
            Goal goal = await _repository.GetByIdAsync(updateGoal.Id);
            goal.Changes.Add(new Change
            {
                ChangerId = updateGoal.ChangerId,
                ChangeTime = DateTime.UtcNow,
                Content = updateGoal.Content
            });
            goal.OwnerId = updateGoal.OwnerId;
            goal.Content = updateGoal.Content;
            goal.Status = updateGoal.Status;
            goal.LastChangeTime = DateTime.UtcNow;
            

            try
            {
                await _repository.UpdateAsync(goal);
                return new UpdateGoalResponse
                {
                    Goal = goal,
                    Success = true
                };
            }
            catch (UpdateGoalException e)
            {
                return new UpdateGoalResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpGet("/getByIdGoal/{id:int}")]
        public async Task<GetByIdGoalResponse> GetByIdGoal([FromRoute] int id)
        {
            try
            {
                var goal = await _repository.GetByIdAsync(id);
                return new GetByIdGoalResponse
                {
                    Goal = goal,
                    Success = true
                };
            }
            catch (GetByIdGoalException e)
            {
                return new GetByIdGoalResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpGet("/getGoals")]
        public async Task<GetGoalsResponse> GetGoals([FromQuery] GetGoals getGoals)
        {
            if (getGoals.ChangerId == null && getGoals.OwnerId == null && getGoals.CreationTime == null &&
                getGoals.LastChangeTime == null)
            {
                try
                {
                    var goals = await _repository.GetByPageAsync(getGoals.Page);
                    return new GetGoalsResponse
                    {
                        Goals = goals,
                        Success = true
                    };
                }
                catch (GetGoalsException e)
                {
                    return new GetGoalsResponse
                    {
                        Success = false,
                        Error = e.Message
                    };
                }
            }
            else if (getGoals.CreationTime != null)
            {
                try
                {
                    var goals = await _repository.GetFilteredPageAsync(
                        g=> g.CreationTime.Date == getGoals.CreationTime,getGoals.Page);
                    return new GetGoalsResponse
                    {
                        Goals = goals,
                        Success = true
                    };
                }
                catch (GetGoalsException e)
                {
                    return new GetGoalsResponse
                    {
                        Success = false,
                        Error = e.Message
                    };
                }
            }
            else if (getGoals.LastChangeTime != null)
            {
                try
                {
                    var goals = await _repository.GetFilteredPageAsync(
                        g=> g.LastChangeTime.Date == getGoals.LastChangeTime, getGoals.Page);
                    return new GetGoalsResponse
                    {
                        Goals = goals,
                        Success = true
                    };
                }
                catch (GetGoalsException e)
                {
                    return new GetGoalsResponse
                    {
                        Success = false,
                        Error = e.Message
                    };
                }
            }
            else if (getGoals.OwnerId != null)
            {
                try
                {
                    var goals = await _repository.GetFilteredPageAsync(
                        g=> g.OwnerId == getGoals.OwnerId,getGoals.Page);
                    return new GetGoalsResponse
                    {
                        Goals = goals,
                        Success = true
                    };
                }
                catch (GetGoalsException e)
                {
                    return new GetGoalsResponse
                    {
                        Success = false,
                        Error = e.Message
                    };
                }
            }
            else
            {
                try
                {
                    var goals = await _repository.GetFilteredPageAsync(
                        g=> g.Changes.Exists(c => c.ChangerId == getGoals.ChangerId), getGoals.Page);
                    return new GetGoalsResponse
                    {
                        Goals = goals,
                        Success = true
                    };
                }
                catch (GetGoalsException e)
                {
                    return new GetGoalsResponse
                    {
                        Success = false,
                        Error = e.Message
                    };
                }
            }
        }

        [HttpGet("/getSlavesGoals/{id:int}")]
        public async Task<GetGoalsResponse> GetSlavesGoalsAsync([FromRoute] int id)
        {
            Employee employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return new GetGoalsResponse()
                {
                    Success = false,
                    Error = "Employee not found"
                };

            var goals = employee.Slaves.SelectMany(slave => slave.Goals).ToList();

            return new GetGoalsResponse()
            {
                Success = true,
                Goals = goals
            };
        }
    }
}