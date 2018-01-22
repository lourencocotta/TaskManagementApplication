using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementApplication.Models
{
    public class TaskListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SelectList ListTaskList { get; set; }

    }
}