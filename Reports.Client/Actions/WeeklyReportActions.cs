using System;
using System.Net.Http;
using System.Net.Http.Json;
using Reports.Core.Models;
using Reports.Core.Requests.ReportRequests;
using Reports.Core.Responses.ReportResponses;

namespace Reports.Client.Actions
{
    public class WeeklyReportActions
    {
        public static void CreateWeeklyReport()
        {
            Console.WriteLine("Введите описание отчета:");
            string content = Console.ReadLine();
            HttpClient client = new HttpClient();
            var result = client.PostAsync(Parameters.URL + "/createWeeklyReport",
                JsonContent.Create<CreateWeeklyReport>(new CreateWeeklyReport
                {
                    Content = content,
                    OwnerId = 1
                })).GetAwaiter().GetResult();
            
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }

            CreateWeeklyReportResponse response =
                result.Content.ReadFromJsonAsync<CreateWeeklyReportResponse>().GetAwaiter().GetResult();
            if (!response.Success)
            {
                Console.WriteLine("Weekly отчет не может быть создан!");
            }
            else
            {
                Console.WriteLine("Weekly отчет успешно создан с id: {0}", response.WeeklyReport.Id);
            }
        }

        public static void UpdateWeeklyReport()
        {
            Console.WriteLine("Введите id отчета, данные которого хотите обновить.");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите статус отчета: 0 - открыт; 1 - в процессе выполнения; 2 - закончен");
            Status status = (Status) Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите описание отчета:");
            string content = Console.ReadLine();

            HttpClient client = new HttpClient();
            var result = client.PostAsync(Parameters.URL + "/updateWeeklyReport", JsonContent.Create<UpdateWeeklyReport>(new UpdateWeeklyReport
            {
                Id = id,
                Status = status,
                Content = content,
                ChangerId = 1
            })).GetAwaiter().GetResult();
            
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }

            UpdateWeeklyReportResponse response =
                result.Content.ReadFromJsonAsync<UpdateWeeklyReportResponse>().GetAwaiter().GetResult();
            if (!response.Success)
            {
                Console.WriteLine("Отчет не может быть обновлен!");
            }
            else
            {
                Console.WriteLine("Отчет с id: {0} успешно обновлен!", response.WeeklyReport.Id);
            }
        }

        public static void GetWeeklyById()
        {
            Console.WriteLine("Введите id отчета, информацию о котором вы хотите найти.");
            string id = Console.ReadLine();
            HttpClient client = new HttpClient();

            var result = client.GetAsync(Parameters.URL + "/getByIdWeeklyReport/" + id).GetAwaiter().GetResult();

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }
            GetByIdWeeklyReportResponse response =
                result.Content.ReadFromJsonAsync<GetByIdWeeklyReportResponse>().GetAwaiter().GetResult();
            
            if (!response.Success || response.WeeklyReport == null)
            {
                Console.WriteLine("Отчет не найдена!");
            }
            else
            {
                Console.WriteLine("Отчет с id: {0} найден:", id);
                Console.WriteLine("Статус отчета: {0}", response.WeeklyReport.Status);
                Console.WriteLine("Описание отчета: {0}", response.WeeklyReport.Content);
                Console.WriteLine("Id ответственного за отчет сотрудника: {0}", response.WeeklyReport.OwnerId);
                Console.WriteLine("Список daily, входящих в отчет:");
                foreach (var daily in response.WeeklyReport.DailyReports)
                {
                    Console.WriteLine("------id: {0}; Статус: {1}; Описание: {2}", daily.Id, daily.Status, daily.Content);
                }
                Console.WriteLine("Список изменений: ");
                foreach(Change c in response.WeeklyReport.Changes)
                {
                    Console.WriteLine("------Время изменения: {0}, ID изменявшего: {1}, Описание: {2}", 
                        c.ChangeTime, c.ChangerId, c.Content);
                }
            }
        }

        public static void AddDailyInWeekly()
        {
            Console.WriteLine("Введите id отчета, в который хотите добавить daily.");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите id daily, который хотите добавить.");
            int dailyId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите описание изменения:");
            string change = Console.ReadLine();
            HttpClient client = new HttpClient();
            var result = client.PostAsync(Parameters.URL + "/addDailyReportInWeeklyReport", JsonContent.Create<AddDailyReportInWeeklyReport>(new AddDailyReportInWeeklyReport
            {
                Id = id,
                DailyId = dailyId,
                Change = change,
                ChangerId = 1
            })).GetAwaiter().GetResult();
            
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            } 
            AddDailyReportInWeeklyReportResponse response =
                result.Content.ReadFromJsonAsync<AddDailyReportInWeeklyReportResponse>().GetAwaiter().GetResult();
            if (!response.Success)
            {
                Console.WriteLine("Daily не может быть добавлен!");
            }
            else
            {
                Console.WriteLine("Daily с id: {0} успешно добавлена в Weekly с id: {1}!", dailyId, response.WeeklyReport.Id);
            }
        }

        public static void GetAllGoals()
        {
            Console.WriteLine("Введите id отчета, в котором вы хотите получить задачи.");
            string id = Console.ReadLine();
            HttpClient client = new HttpClient();

            var result = client.GetAsync(Parameters.URL + "/getGoalsInWeeklyReport/" + id).GetAwaiter().GetResult();

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }
            GetGoalsInWeeklyReportResponse response =
                result.Content.ReadFromJsonAsync<GetGoalsInWeeklyReportResponse>().GetAwaiter().GetResult();
            
            if (!response.Success || response.Goals == null)
            {
                Console.WriteLine("Задачи не найдены!");
            }
            else
            {
                foreach (var goal in response.Goals)
                {
                    Console.WriteLine("id: {0}; Время создания: {1}; Описание: {2}; Ответственный: {3}", 
                        goal.Id, goal.CreationTime, goal.Content,goal.OwnerId);
                }
            }
            
        }
    }
}