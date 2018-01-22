using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskManagementApplication.Models
{
    public interface IApplicationDbContext
    {
        IDbSet<TaskList> TaskLists { get; set; }
        IDbSet<Task> Tasks { get; set; }

        int SaveChanges();

    }

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<TaskList> TaskLists { get; set; }
        public IDbSet<Task> Tasks { get; set; }

    }

    public class FakeApplicationDBContext : IApplicationDbContext
    {
        public IDbSet<TaskList> TaskLists { get; set; }
        public IDbSet<Task> Tasks { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}