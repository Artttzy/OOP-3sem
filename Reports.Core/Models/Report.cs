using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reports.Core.Models
{
    public abstract class Report
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }
        public DateTime LastChangeTime { get; set; }
        [Required]
        public int OwnerId { get; set; }
        
        public Employee Owner { get; set; }

        [Required]
        public Status Status { get; set; }
        
        public string Content { get; set; }
        
        public List<Change> Changes { get; set; } = new List<Change>();
    }
}