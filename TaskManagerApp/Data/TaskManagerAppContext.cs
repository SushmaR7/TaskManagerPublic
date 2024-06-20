using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Models;
using Microsoft.IdentityModel;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TaskManagerApp.Data
{
    public class TaskManagerAppContext : IdentityDbContext<UserDetail>
    {
        public TaskManagerAppContext(DbContextOptions<TaskManagerAppContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserDetail>().ToTable("UsersDetails");

        }

        public DbSet<TaskManagerApp.Models.TaskHome> TaskHome { get; set; } = default!;
        public DbSet<TaskManagerApp.Models.UserDetail> UserDetail { get; set; } = default!;



    }
}
