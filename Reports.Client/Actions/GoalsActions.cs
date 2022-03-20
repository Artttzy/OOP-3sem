using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using Reports.Core.Models;
using Reports.Core.Requests.GoalRequests;
using Reports.Core.Responses.GoalResponses;

namespace Reports.Client.Actions
{
    public class GoalsActions
    {
        public static void CreateGoal(int ownerId)
        {

            HttpClient client = new HttpClient();
            var result = client.PostAsync(Parameters.URL + "/createGoal", JsonContent.Create<CreateGoal>(new CreateGoal
            {
                OwnerId = ownerId
            })).GetAwaiter().GetResult();
            
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }

            CreateGoalResponse response =
                result.Content.ReadFromJsonAsync<CreateGoalResponse>().GetAwaiter().GetResult();
            if (!response.Success)
            {
                Console.WriteLine("Задача не может быть создана!");
            }
            else
            {
                Console.WriteLine("Задача успешно создана с id: {0}", response.Goal.Id);
            }
        }

        public static void UpdateGoal(int changerId)
        {
            Console.WriteLine("Введите id задачи, данные которой хотите обновить.");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите статус задачи: 0 - открыта; 1 - в процессе выполнения; 2 - выполнена");
            Status status = (Status) Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите описание задачи:");
            string content = Console.ReadLine();
            Console.WriteLine("Введите нового ответственного или оставьте старого.");
            int ownerId = Convert.ToInt32(Console.ReadLine());
            
            HttpClient client = new HttpClient();
            var result = client.PostAsync(Parameters.URL + "/updateGoal", JsonContent.Create<UpdateGoal>(new UpdateGoal
            {
                Id = id,
                Status = status,
                Content = content,
                ChangerId = changerId,
                OwnerId = ownerId
            })).GetAwaiter().GetResult();
            
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }

            UpdateGoalResponse response =
                result.Content.ReadFromJsonAsync<UpdateGoalResponse>().GetAwaiter().GetResult();
            if (!response.Success)
            {
                Console.WriteLine("Задача не может быть обновлена!");
            }
            else
            {
                Console.WriteLine("Задача с id: {0} успешно обновлена!", response.Goal.Id);
            }
        }

        public static void GetByIdGoal()
        {
            Console.WriteLine("Введите id задачи, информацию о которой вы хотите найти.");
            string id = Console.ReadLine();
            HttpClient client = new HttpClient();

            var result = client.GetAsync(Parameters.URL + "/getByIdGoal/" + id).GetAwaiter().GetResult();

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }
            GetByIdGoalResponse response =
                result.Content.ReadFromJsonAsync<GetByIdGoalResponse>().GetAwaiter().GetResult();
            
            if (!response.Success || response.Goal == null)
            {
                Console.WriteLine("Задача не найдена!");
            }
            else
            {
                Console.WriteLine("Задача с id: {0} найдена:", id);
                Console.WriteLine("Время создания: {0}", response.Goal.CreationTime);
                Console.WriteLine("Время последнего изменения: {0}", response.Goal.LastChangeTime);
                Console.WriteLine("Статус задачи: {0}", response.Goal.Status);
                Console.WriteLine("Описание задачи: {0}", response.Goal.Content);
                Console.WriteLine("Id ответственного за задачу сотрудник: {0}", response.Goal.OwnerId);
                Console.WriteLine("Список изменений: ");
                foreach(Change c in response.Goal.Changes)
                {
                    Console.WriteLine("------Время изменения: {0}, ID изменявшего: {1}, Описание: {2}", 
                        c.ChangeTime, c.ChangerId, c.Content);
                }
            }
        }

        public static void GetFilteredAndPagedGoals()
        {
            Console.WriteLine("Как вы хотите найти задачи: 1 - По дате создания; 2 - По дате последнего изменения; " +
                              "3 - По id ответственного сотрудника; 4 - По id сотрудника, вносившего изменения; 5 - Без филтров");
            int select = Convert.ToInt32(Console.ReadLine());
            DateTime creationTime = DateTime.Now;
            DateTime lastChangeTime = DateTime.Now;
            int? ownerId = null;
            int? changerId = null;
            switch (select)
            {
                case 1:
                    Console.WriteLine("Введите дату создания в формате 'дд.мм.гггг'.");
                    DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", null,
                        DateTimeStyles.None, out creationTime);
                    break;
                case 2:
                    Console.WriteLine("Введите дату последнего изменения в формате 'дд.мм.гггг'.");
                    DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", null,
                        DateTimeStyles.None, out lastChangeTime);
                    break;
                case 3:
                    Console.WriteLine("Введите id ответственного сотрудника.");
                    ownerId = Convert.ToInt32(Console.ReadLine());
                    break;
                case 4:
                    Console.WriteLine("Введите id сотрудника, вносившего изменения.");
                    changerId = Convert.ToInt32(Console.ReadLine());
                    break;
                case 5:
                    break;
            }
            Console.WriteLine("Введите номер страницы.");
            int page = Convert.ToInt32(Console.ReadLine());
            HttpClient client = new HttpClient();
            var result = client.GetAsync(Parameters.URL 
                                         + "/getGoals?page=" + page + 
                                         "&lastChangeTime=" + lastChangeTime + "&creationTime=" + creationTime +
                                         "&ownerId=" + ownerId + "&changerId=" + changerId).GetAwaiter().GetResult();
            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }

            GetGoalsResponse response =
                result.Content.ReadFromJsonAsync<GetGoalsResponse>().GetAwaiter().GetResult();
            if (!response.Success || response.Goals.Count == 0)
            {
                Console.WriteLine("Задачи невозможно найти!");
            }
            else
            {
                Console.WriteLine("Страница: {0}", page);
                foreach (var goal in response.Goals)
                {
                    Console.WriteLine("id: {0}; Время создания: {1}; Описание: {2}; Ответственный: {3}", 
                        goal.Id, goal.CreationTime, goal.Content,goal.OwnerId);
                }
            }
        }

        public static void GetSlavesGoals(int ownerId)
        {
            HttpClient client = new HttpClient();

            var result = client.GetAsync(Parameters.URL + "/getSlavesGoals/" + ownerId).GetAwaiter().GetResult();

            if (!result.IsSuccessStatusCode)
            {
                Console.WriteLine("Ошибка при выполнении запроса!");
                return;
            }
            GetGoalsResponse response =
                result.Content.ReadFromJsonAsync<GetGoalsResponse>().GetAwaiter().GetResult();
            if (!response.Success || response.Goals.Count == 0)
            {
                Console.WriteLine("Задачи невозможно найти!");
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