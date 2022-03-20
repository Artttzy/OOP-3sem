using System;
using System.Net.Http;
using System.Net.Http.Json;
using Reports.Core.Models;
using Reports.Core.Requests.GoalRequests;
using Reports.Core.Requests.ReportRequests;
using Reports.Core.Responses.GoalResponses;
using Reports.Core.Responses.ReportResponses;

namespace Reports.Client
{
    public class DailyReportsActions
    {
        public static void CreateDailyReport(int ownerId)
        {
            Console.WriteLine("Введите описание отчета:");
            string content = Console.ReadLine();
            HttpClient client = new HttpClient();
            var result = client.PostAsync(Parameters.URL + "/createDailyReport",
                JsonContent.Create<CreateDailyReport>(new CreateDailyReport
            {
                Content = content,
                OwnerId = ownerId
            })).GetAwaiter().GetResult();
            
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }

            CreateDailyReportResponse response =
                result.Content.ReadFromJsonAsync<CreateDailyReportResponse>().GetAwaiter().GetResult();
            if (!response.Success)
            {
                Console.WriteLine("Daily отчет не может быть создан!");
            }
            else
            {
                Console.WriteLine("Daily отчет успешно создан с id: {0}", response.DailyReport.Id);
            }
        }

        public static void UpdateDailyReport(int changerId)
        {
            Console.WriteLine("Введите id отчета, данные которого хотите обновить.");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите статус отчета: 0 - открыт; 1 - в процессе выполнения; 2 - закончен");
            Status status = (Status) Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите описание отчета:");
            string content = Console.ReadLine();

            HttpClient client = new HttpClient();
            var result = client.PostAsync(Parameters.URL + "/updateDailyReport", JsonContent.Create<UpdateDailyReport>(new UpdateDailyReport
            {
                Id = id,
                Status = status,
                Content = content,
                ChangerId = changerId
            })).GetAwaiter().GetResult();
            
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }

            UpdateDailyReportResponse response =
                result.Content.ReadFromJsonAsync<UpdateDailyReportResponse>().GetAwaiter().GetResult();
            if (!response.Success)
            {
                Console.WriteLine("Отчет не может быть обновлен!");
            }
            else
            {
                Console.WriteLine("Отчет с id: {0} успешно обновлен!", response.DailyReport.Id);
            }
        }

        public static void GetByIdDailyReport()
        {
            Console.WriteLine("Введите id отчета, информацию о котором вы хотите найти.");
            string id = Console.ReadLine();
            HttpClient client = new HttpClient();

            var result = client.GetAsync(Parameters.URL + "/getByIdDailyReport/" + id).GetAwaiter().GetResult();

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }
            GetByIdDailyReportResponse response =
                result.Content.ReadFromJsonAsync<GetByIdDailyReportResponse>().GetAwaiter().GetResult();
            
            if (!response.Success || response.DailyReport == null)
            {
                Console.WriteLine("Задача не найдена!");
            }
            else
            {
                Console.WriteLine("Отчет с id: {0} найден:", id);
                Console.WriteLine("Статус отчета: {0}", response.DailyReport.Status);
                Console.WriteLine("Описание отчета: {0}", response.DailyReport.Content);
                Console.WriteLine("Id ответственного за отчет сотрудника: {0}", response.DailyReport.OwnerId);
                Console.WriteLine("Список id задач, входящих в отчет:");
                foreach (var goal in response.DailyReport.Goals)
                {
                    Console.WriteLine("------id: {0}; Статус: {1}; Описание: {2}", goal.Id, goal.Status, goal.Content);
                }
                Console.WriteLine("Список изменений: ");
                foreach(Change c in response.DailyReport.Changes)
                {
                    Console.WriteLine("------Время изменения: {0}, ID изменявшего: {1}, Описание: {2}", 
                        c.ChangeTime, c.ChangerId, c.Content);
                }
            }
        }

        public static void AddGoalInDailyReport(int changerId)
        {
            Console.WriteLine("Введите id отчета, в который хотите добавить задачу.");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите id задачи, которую хотите добавить.");
            int goalId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите описание изменения:");
            string change = Console.ReadLine();
            
            HttpClient client = new HttpClient();
            var result = client.PostAsync(Parameters.URL + "/addGoalInDailyReport", JsonContent.Create<AddGoalInDailyReport>(new AddGoalInDailyReport
            {
                Id = id,
                GoalId = goalId,
                Change = change,
                ChangerId = changerId
            })).GetAwaiter().GetResult();
            
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            } 
            AddGoalInDailyReportResponse response =
                result.Content.ReadFromJsonAsync<AddGoalInDailyReportResponse>().GetAwaiter().GetResult();
            if (!response.Success)
            {
                Console.WriteLine("Задача не может быть добавлена!");
            }
            else
            {
                Console.WriteLine("Задача с id: {0} успешно добавлена в отчет с id: {1}!", goalId, response.DailyReport.Id);
            }
        }

        public static void GetSlavesDailyReports(int ownerId)
        {
            Console.WriteLine("Какие отчеты вы хотите получить?: 1 - законченные; 0 - незаконченные");
            bool readiness = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
            HttpClient client = new HttpClient();
            var result = client.PostAsync(Parameters.URL + "/getSlavesDailyReportsGoals",
                JsonContent.Create<GetSlavesDailyReports>(new GetSlavesDailyReports
                {
                    OwnerId = ownerId,
                   Readiness = readiness
                })).GetAwaiter().GetResult();

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }
            GetDailyReportsResponse response =
                result.Content.ReadFromJsonAsync<GetDailyReportsResponse>().GetAwaiter().GetResult();
            if (!response.Success || response.DailyReports.Count == 0)
            {
                Console.WriteLine("Отчеты невозможно найти!");
            }
            else
            {
                foreach (var dailyReport in response.DailyReports)
                {
                    Console.WriteLine("id: {0}; Время создания: {1}; Описание: {2}; Ответственный: {3}", 
                        dailyReport.Id, dailyReport.CreationTime, dailyReport.Content, dailyReport.OwnerId);
                }
            }
        }
    }
}