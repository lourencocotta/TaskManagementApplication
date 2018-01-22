using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementApplication.Models;
using DataTables.AspNet.Mvc5;
using DataTables.AspNet.Core;
using System.Data.Entity.Validation;

namespace TaskManagementApplication.Controllers
{

    public class TaskController : Controller
    {
        private IApplicationDbContext db;

        public TaskController()
        {
            db = new ApplicationDbContext();
        }

        public TaskController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("CreateTask", "Task");
        }

        // GET: Task/CreateTask
        public ActionResult CreateTask()
        {
            var model = new TaskViewModel();

            model.ListTaskList = GetTaskListSelectList();
            return View(model);
        }

        // POST: Task/CreateTask
        [HttpPost]
        public ActionResult CreateTask(TaskViewModel TaskVM)
        {
            TaskVM.ListTaskList = GetTaskListSelectList();

            if (TaskVM.StartDate > TaskVM.FinishDate)
            {
                ModelState.AddModelError("", "Finish date must be greater than start date.");
                return View("CreateTask", TaskVM);
            }

            if (!ModelState.IsValid)
                return View("_CreateTaskPartial", TaskVM);

            try
            {
                Task Task = MapToModel(TaskVM);

                db.Tasks.Add(Task);
                var task = db.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                string error= string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                   
                    error = $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:";
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error = error + $"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"";
                    }
                }

                ModelState.AddModelError("", error);
                return View("CreateTask", TaskVM);

            }

            return RedirectToAction("Index", "Task");

            //return Content("success");
        }

        private SelectList GetTaskListSelectList(object selectedValue = null)
        {
            return new SelectList(db.TaskLists
                                            .Select(x => new { x.Id, x.Name }),
                                                "Id",
                                                "Name", selectedValue);
        }

        private Task MapToModel (TaskViewModel taskVM)
        {
            Task Task = new Task()
            {
                TaskName = taskVM.TaskName,
                StartDate = taskVM.StartDate,
                FinishDate = taskVM.FinishDate,
                TaskListId = taskVM.TaskListId
            };

            return Task;
        }

        
    }
}
