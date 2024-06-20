using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace TaskManagerApp.Models
{
    public class TaskHome
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        [Required]
        public string? TaskName { get; set; }
        [Required]
        public string? TaskDescription { get; set; }
        [Required]
        public DateOnly? Startdate { get; set; }
        [Required]
        public DateOnly? Deadline { get; set; }

        [Required]
        public TodoOptions TaskStatusId { get; set; }
        public string UserId { get; set; }
    }
    public enum TodoOptions
    {
        ToDo, InProgress, Complete
    }
}
