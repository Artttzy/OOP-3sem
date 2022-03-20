using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Models
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }
        
        public int? ManagerId { get; set; }
        
        public Employee Manager { get; set; }
        public List<Employee> Slaves { get; set; } = new List<Employee>();

        public List<Goal> Goals { get; set; } = new List<Goal>();
        
        public List<DailyReport> DailyReports { get; set; } = new List<DailyReport>();
        public List<WeeklyReport> WeeklyReports { get; set; } = new List<WeeklyReport>();
    }
}