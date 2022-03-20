using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reports.Core.Models;
using Reports.Core.Requests;
using Reports.Core.Responses;
using Reports.Exceptions;
using Reports.Interfaces;

namespace Reports.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IRepository<Employee> _repository;
        private readonly IPagedFilter<Employee> _filter;
        private readonly IRepository<Goal> _goalsRepository;

        public EmployeeController(IRepository<Employee> repository, IPagedFilter<Employee> filter,
            IRepository<Goal> _goalsRepository)
        {
            _repository = repository;
            _filter = filter;
            this._goalsRepository = _goalsRepository;
        }

        [HttpPost("/createEmployee")]
        public async Task<CreateEmployeeResponse> CreateEmployee([FromBody] CreateEmployee createEmployee)
        {
            var employee = new Employee
            {
                Name = createEmployee.Name,
                Surname = createEmployee.Surname,
                ManagerId = createEmployee.ManagerId
            };

            try
            {
                await _repository.CreateAsync(employee);

                return new CreateEmployeeResponse
                {
                    Employee = employee,
                    Success = true
                };
            }
            catch (CreateEmployeeException e)
            {
                return new CreateEmployeeResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpDelete("/deleteEmployee/{id:int}")]
        public async Task<DeleteEmployeeResponse> DeleteEmployee([FromRoute] int id)
        {
            Employee toDelete = await _repository.GetByIdAsync(id);

            if (toDelete == null || toDelete.ManagerId == null)
                return new DeleteEmployeeResponse()
                {
                    Error = "Not found",
                    Success = false
                };

            try
            {
                foreach (Employee slave in toDelete.Slaves)
                {
                    slave.ManagerId = toDelete.ManagerId;
                    await _repository.UpdateAsync(slave);
                }

                foreach (Goal goal in toDelete.Goals)
                {
                    goal.OwnerId = toDelete.ManagerId ?? throw new NullReferenceException();
                    await _goalsRepository.UpdateAsync(goal);
                }

                await _repository.RemoveAsync(toDelete);
                return new DeleteEmployeeResponse
                {
                    Success = true
                };
            }
            catch (DeleteEmployeeException e)
            {
                return new DeleteEmployeeResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpPost("/updateEmployee")]
        public async Task<UpdateEmployeeResponse> UpdateEmployee([FromBody] UpdateEmployee updateEmployee)
        {
            var employee = new Employee
                {
                    Id = updateEmployee.Id,
                    Name = updateEmployee.Name,
                    Surname = updateEmployee.Surname
                };


            try
            {
                await _repository.UpdateAsync(employee);
                return new UpdateEmployeeResponse
                {
                    Employee = employee,
                    Success = true
                };
            }
            catch (UpdateEmployeeException e)
            {
                return new UpdateEmployeeResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpGet("/getByIdEmployee/{id:int}")]
        public async Task<GetByIdResponse> GetById([FromRoute] int id)
        {
            try
            {
                var employee = await _repository.GetByIdAsync(id);
                return new GetByIdResponse
                {
                    Employee = employee,
                    Success = true
                };
            }
            catch (GetByIdException e)
            {
                return new GetByIdResponse
                {
                    Success = false,
                    Error = e.Message
                };
            }
        }

        [HttpPost("/getEmployees")]
        public async Task<GetEmployeesResponse> GetEmployees([FromBody] GetEmployees getEmployees)
        {
            if (getEmployees.Surname == null && getEmployees.ManagerId == null)
            {
                try
                {
                    var employees = await _repository.GetByPageAsync(getEmployees.Page);
                    return new GetEmployeesResponse
                    {
                        Employees = employees,
                        Success = true
                    };
                }
                catch (GetByPageException e)
                {
                    return new GetEmployeesResponse
                    {
                        Success = false,
                        Error = e.Message
                    };
                }
            }
            else if (getEmployees.Surname == null)
            {
                try
                {
                    var employees = await _repository.GetFilteredPageAsync(i => i.ManagerId == getEmployees.ManagerId,
                        getEmployees.Page);
                    return new GetEmployeesResponse
                    {
                        Employees = employees,
                        Success = true
                    };
                }
                catch (GetByPageException e)
                {
                    return new GetEmployeesResponse
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
                    var employees =
                        await _repository.GetFilteredPageAsync(i => i.Surname == getEmployees.Surname,
                            getEmployees.Page);
                    return new GetEmployeesResponse
                    {
                        Employees = employees,
                        Success = true
                    };
                }
                catch (GetByPageException e)
                {
                    return new GetEmployeesResponse
                    {
                        Success = false,
                        Error = e.Message
                    };
                }
            }
        }
    }
}