using System;
using System.Reflection.PortableExecutable;
using Reports.Client.Actions;
using Reports.Core.Models;
using Reports.Core.Requests;

namespace Reports.Client
{
    class Program
    {
        public static void Main()
        {
            int mode = 0;
            int select = 0;
            while (mode != 4)
            {
                Console.WriteLine("С каким разделом вы хотите работать? : 1 - Сотрудники; 2 - Задачи; 3 - Отчеты; 4 - Выход");
                mode = Convert.ToInt32(Console.ReadLine());
                switch (mode)
                {
                    case 1:
                        select = 0;
                        while (select != 6)
                        {
                            Console.WriteLine("Что вы хотите сделать? : 1 - Создать сотрудника; 2 - Обновить данные сотрудника" +
                                              "; 3 - Удалить сотрудника; 4 - Получить сотрудника по ID; " +
                                              "5 - Получить сотрудников по фильтрам и страницам; 6 - Выход");
                            select = Convert.ToInt32(Console.ReadLine());
                            switch (select)
                            {
                                case 1:
                                    EmployeesActions.CreateEmployee();
                                    break;
                                case 2:
                                    EmployeesActions.UpdateEmployee();
                                    break;
                                case 3:
                                    EmployeesActions.DeleteEmployee();
                                    break;
                                case 4:
                                    EmployeesActions.GetByIdEmployee();
                                    break;
                                case 5:
                                    EmployeesActions.GetFilteredAndPagedEmployees();
                                    break;
                                case 6:
                                    break;
                                    
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("Введите id сотрудника от лица которого будет вестись работа.");
                        int mangerId = Convert.ToInt32(Console.ReadLine());
                        select = 0;
                        while (select != 6)
                        {
                            Console.WriteLine(
                                "Что вы хотите сделать? : 1 - Создать задачу; 2 - Обновить данные задачи" +
                                "; 3 - Получить задачу по ID; 4 - Получить задачи по фильтрам и страницам; " +
                                "5 - Получить задачи подчиненных сотрудника ;6 - Выход");
                            select = Convert.ToInt32(Console.ReadLine());
                            switch (select)
                            {
                                case 1:
                                    GoalsActions.CreateGoal(mangerId);
                                    break;
                                case 2:
                                    GoalsActions.UpdateGoal(mangerId);
                                    break;
                                case 3:
                                    GoalsActions.GetByIdGoal();
                                    break;
                                case 4:
                                    GoalsActions.GetFilteredAndPagedGoals();
                                    break;
                                case 5:
                                    GoalsActions.GetSlavesGoals(mangerId);
                                    break;
                                case 6:
                                    break;
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("Введите id сотрудника от лица которого будет вестись работа.");
                        int managerId = Convert.ToInt32(Console.ReadLine());
                        if (managerId != 1)
                        {
                            select = 0;
                            while (select != 6)
                            {
                                Console.WriteLine(
                                    "Что вы хотите сделать? : 1 - Создать daily отчет; 2 - Обновить данные daily отчета" +
                                    "; 3 - Получить daily отчет по ID; 4 - Добавить задачу в daily отчет; " +
                                    "5 - Получить daily отчеты подчиненных сотрудника ;6 - Выход");
                                select = Convert.ToInt32(Console.ReadLine());
                                switch (select)
                                {
                                    case 1:
                                        DailyReportsActions.CreateDailyReport(managerId);
                                        break;
                                    case 2:
                                        DailyReportsActions.UpdateDailyReport(managerId);
                                        break;
                                    case 3:
                                        DailyReportsActions.GetByIdDailyReport();
                                        break;
                                    case 4:
                                        DailyReportsActions.AddGoalInDailyReport(managerId);
                                        break;
                                    case 5:
                                        DailyReportsActions.GetSlavesDailyReports(managerId);
                                        break;
                                    case 6:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            select = 0;
                            while (select != 6)
                            {
                                Console.WriteLine(
                                    "Добро пожаловать, Тимлид! Что вы хотите сделать? : 1 - Создать weekly отчет; 2 - Обновить данные weekly отчета" +
                                    "; 3 - Получить weekly отчет по ID; 4 - Добавить daily в weekly отчет; " +
                                    "5 - Получить задачи за весь спринт ;6 - Выход");
                                select = Convert.ToInt32(Console.ReadLine());
                                switch (select)
                                {
                                    case 1:
                                        WeeklyReportActions.CreateWeeklyReport();
                                        break;
                                    case 2:
                                        WeeklyReportActions.UpdateWeeklyReport();
                                        break;
                                    case 3:
                                        WeeklyReportActions.GetWeeklyById();
                                        break;
                                    case 4:
                                        WeeklyReportActions.AddDailyInWeekly();
                                        break;
                                    case 5:
                                        WeeklyReportActions.GetAllGoals();
                                        break;
                                    case 6:
                                        break;
                                }
                            }
                        }

                            
                        break;
                    case 4:
                        break;
                }
            }
        }
    }
}