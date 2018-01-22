using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementApplication.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Finish Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FinishDate { get; set; }

        [Display(Name = "Task List")]
        public int TaskListId { get; set; }
        
        public string TaskListName { get; set; }

        public SelectList ListTaskList { get; set; }

        public static TaskViewModel FromModel(Task model)
        {
            return new TaskViewModel
            {
                TaskName = model.TaskName,
                StartDate = model.StartDate,
                FinishDate = model.FinishDate,
                TaskListId = model.TaskListId
            };
        }

    }

}