using System;
using System.ComponentModel.DataAnnotations;

namespace Reports.Core.Models
{
    public class Change
    {
        public int Id { get; set; }
        [Required]
        public DateTime ChangeTime { get; set; }
        [Required]
        public int ChangerId { get; set; }
        public string Content { get; set; }
    }
}