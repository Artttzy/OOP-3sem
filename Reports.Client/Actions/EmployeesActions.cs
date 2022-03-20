using System;
using System.Net.Http;
using System.Net.Http.Json;
using Reports.Core.Requests;
using Reports.Core.Responses;

namespace Reports.Client
{
    public class EmployeesActions
    {
        public static void CreateEmployee()
        {
            Console.WriteLine("Введите имя сотрудник.");
            string name = Console.ReadLine();
            Console.WriteLine("Введите фамилию сотрудника.");
            string surname = Console.ReadLine();
            Console.WriteLine("Введите id руководителя. Введите 'null', если сотрудник тимлид.");
            int? managerId;
            string managerIds = Console.ReadLine();
            if (managerIds == "null")
            {
                managerId = null;
            }
            else
            {
                managerId = Convert.ToInt32(managerIds);
            }

            HttpClient client = new HttpClient();
            var result = client.PostAsync(Parameters.URL + "/createEmployee", JsonContent.Create<CreateEmployee>(new CreateEmployee
            {
                Name = name,
                Surname = surname,
                ManagerId = managerId
            })).GetAwaiter().GetResult();

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }

            CreateEmployeeResponse response =
                result.Content.ReadFromJsonAsync<CreateEmployeeResponse>().GetAwaiter().GetResult();
            if (!response.Success)
            {
                Console.WriteLine("Сотрудник не может быть создан!");
            }
            else
            {
                Console.WriteLine("Сотрудник успешно создан с id: {0}", response.Employee.Id);
            }
        }

        public static void UpdateEmployee()
        {
            Console.WriteLine("Введите id сотрудника, данные которого хотите обновить.");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите новое имя сотрудника.");
            string name = Console.ReadLine();
            Console.WriteLine("Введите новую фамилию сотрудника.");
            string surname = Console.ReadLine();     
            Console.WriteLine("Введите id нового руководителя или 'null', если это тимлид.");
            int? managerId;
            string managerIds = Console.ReadLine();
            if (managerIds == "null")
            {
                managerId = null;
            }
            else
            {
                managerId = Convert.ToInt32(managerIds);
            }
            
            HttpClient client = new HttpClient();
            var result = client.PostAsync(Parameters.URL + "/updateEmployee", JsonContent.Create< UpdateEmployee>(new UpdateEmployee
            {
                Id = id,
                Name = name,
                Surname = surname,
                ManagerId = managerId
            })).GetAwaiter().GetResult();

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }
            UpdateEmployeeResponse response =
                result.Content.ReadFromJsonAsync<UpdateEmployeeResponse>().GetAwaiter().GetResult();
            if (!response.Success)
            {
                Console.WriteLine("Данные сотрудника не могут быть обновлены!");
            }
            else
            {
                Console.WriteLine("Данные сотрудника с id: {0} успешно обновлены!", response.Employee.Id);
            }
        }

        public static void DeleteEmployee()
        {
            Console.WriteLine("Введите id сотрудника, которого вы хотите удалить.");
            string id = Console.ReadLine();
            HttpClient client = new HttpClient();

            var result = client.DeleteAsync(Parameters.URL + "/deleteEmployee/" + id).GetAwaiter().GetResult();

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }
            DeleteEmployeeResponse response =
                result.Content.ReadFromJsonAsync<DeleteEmployeeResponse>().GetAwaiter().GetResult();
            if (!response.Success)
            {
                Console.WriteLine("Сотрудник не может быть удален!");
            }
            else
            {
                Console.WriteLine("Сотрудник с id: {0} удален!", id);
            }
        }

        public static void GetByIdEmployee()
        {
            Console.WriteLine("Введите id сотрудника, информацию о котором вы хотите найти.");
            string id = Console.ReadLine();
            HttpClient client = new HttpClient();

            var result = client.GetAsync(Parameters.URL + "/getByIdEmployee/" + id).GetAwaiter().GetResult();

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }
            GetByIdResponse response =
                result.Content.ReadFromJsonAsync<GetByIdResponse>().GetAwaiter().GetResult();
            if (!response.Success || response.Employee == null)
            {
                Console.WriteLine("Сотрудник не найден!");
            }
            else
            {
                Console.WriteLine("Сотрудник с id: {0} найден:", id);
                Console.WriteLine("Имя: {0}", response.Employee.Name);
                Console.WriteLine("Фамилия: {0}", response.Employee.Surname);
                Console.WriteLine("ID руководителя: {0}", response.Employee.ManagerId);
                Console.Write("Список ID подчиненных сотрудника:");
                foreach (var slave in response.Employee.Slaves)
                {
                    Console.Write(" {0}", slave.Id);
                }
                Console.WriteLine();
                Console.Write("Список ID задач сотрудника:");
                foreach (var goal in response.Employee.Goals)
                {
                    Console.Write(" {0}", goal.Id);
                }
                Console.WriteLine();
                Console.Write("Список ID отчетов сотрудника:");
                foreach (var daily in response.Employee.DailyReports)
                {
                    Console.Write(" {0}", daily.Id);
                }
                Console.WriteLine();
            }
        }

        public static void GetFilteredAndPagedEmployees()
        {
            Console.WriteLine(
                "Как вы хотите найти сотрудников: 1 - по фамилии; 2 - по id руководителя; 3 - без фильтров");
            int select = Convert.ToInt32(Console.ReadLine());
            string surname = null;
            int? managerId = null;
            switch (select)
            {
                case 1:
                    Console.WriteLine("Введите фамилию.");
                    surname = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Введите id руководителя.");
                    managerId = Convert.ToInt32(Console.ReadLine());
                    break;
                case 3:
                    break;
                    
            }
            Console.WriteLine("Введите номер страницы.");
            int page = Convert.ToInt32(Console.ReadLine());
            
            HttpClient client = new HttpClient();

            var result = client.PostAsync(Parameters.URL + "/getEmployees", JsonContent.Create<GetEmployees>(new GetEmployees
            {
                Page = page,
                Surname = surname,
                ManagerId = managerId
            })).GetAwaiter().GetResult();
            
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }

            GetEmployeesResponse response =
                result.Content.ReadFromJsonAsync<GetEmployeesResponse>().GetAwaiter().GetResult();
            if (!response.Success || response.Employees.Count == 0)
            {
                Console.WriteLine("Сотрудников невозможно найти!");
            }
            else
            {
                Console.WriteLine("Страница: {0}", page);
                foreach (var employee in response.Employees)
                {
                    Console.WriteLine("id: {0}; ФИ: {1} {2}; id руководителя: {3}", employee.Id, employee.Surname, employee.Name, employee.ManagerId);
                }
            }
        }
    }
}