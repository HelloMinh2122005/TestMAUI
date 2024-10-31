using System;
using Microsoft.EntityFrameworkCore;
using ClassesStudentsMAUI.Models;

namespace ClassesStudentsMAUI
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
