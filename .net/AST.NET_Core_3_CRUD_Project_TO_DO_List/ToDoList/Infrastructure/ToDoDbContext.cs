using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Infrastructure
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) :base(options)
        {

        }

        public DbSet<ToDoListModel> ToDoLists { get; set; }
    }
}
