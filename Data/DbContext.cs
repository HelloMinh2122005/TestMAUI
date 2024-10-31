using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassesStudentsMAUI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassesStudentsMAUI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=ClassStudent_database.db");
        }
    }
}
