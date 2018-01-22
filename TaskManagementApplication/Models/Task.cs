using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskManagementApplication.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        public string TaskName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime FinishDate { get; set; }

        [ForeignKey("TaskList")]
        public int TaskListId { get; set; }

        public virtual TaskList TaskList { get; set; }


    }
}