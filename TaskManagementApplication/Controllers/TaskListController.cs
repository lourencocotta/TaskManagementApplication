using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementApplication.Models;
using DataTables.AspNet.Mvc5;
using DataTables.AspNet.Core;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace TaskManagementApplication.Controllers
{
    public class TaskListController : Controller
    {
        private IApplicationDbContext db;

        public TaskListController()
        {
            db = new ApplicationDbContext();
        }

        public TaskListController(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTaskList(IDataTablesRequest request)
        {
            var data = db.TaskLists.ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item => _item.Name.Contains(request.Search.Value));

            // Paging filtered data.
            // Paging is rather manual due to in-memmory (IEnumerable) data.
            var dataPage = filteredData.Skip(request.Start).Take(request.Length);

            // Response creation. To create your response you need to reference your request, to avoid
            // request/response tampering and to ensure response will be correctly created.
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            // Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
            // response to a json-compatible content, so DataTables can read it when received.
            var x = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }


        // GET: TaskList/CreateTaskList
        public ActionResult CreateTaskList()
        {
            var model = new TaskListViewModel();
            return View("_CreateTaskListPartial", model);
        }

        // GET: TaskList/CreateTaskList
        public ActionResult GetTasks(TaskListViewModel taskList)
        {
            var model = db.Tasks.Where(x => x.TaskListId == taskList.Id).ToList();
            List<TaskViewModel> viewModel = new List<TaskViewModel>();
            foreach (var item in model)
            {
                viewModel.Add(TaskViewModel.FromModel(item));
            }

            return View("_GridTaskPartial", viewModel);
        }

        // POST: TaskList/CreateTaskList
        [HttpPost]
        public ActionResult CreateTaskList(TaskListViewModel taskListVM)
        {

            if (!ModelState.IsValid)
                return View("_CreateTaskListPartial", taskListVM);

            try
            {
                TaskList taskList = MapToModel(taskListVM);

                db.TaskLists.Add(taskList);
                var task = db.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                string error = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {

                    error = $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:";
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error = error + $"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"";
                    }
                }

                ModelState.AddModelError("", error);
                return View("_CreateTaskListPartial", taskListVM);

            }

            return Content("success");
        }

        private TaskList MapToModel (TaskListViewModel taskListVM)
        {
            TaskList taskList = new TaskList()
            {
                Name = taskListVM.Name
            };

            return taskList;
        }
    }
}
