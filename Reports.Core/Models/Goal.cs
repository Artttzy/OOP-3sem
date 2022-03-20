using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Models
{
    public class Goal
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public DateTime CreationTime { get; set; }
        
        public DateTime LastChangeTime { get; set; }
        
        [Required]
        public int OwnerId { get; set; }
        
        public Employee Owner { get; set; }
        public List<Change> Changes { get; set; } = new List<Change>();
        
        [Required]
        public Status Status { get; set; }
        
        public string Content { get; set; }
    }
}
