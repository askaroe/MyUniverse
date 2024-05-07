using Microsoft.EntityFrameworkCore;
using MyUniverse.Models;
using System;

namespace MyUniverse.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        public DbSet<StudentModel> Students { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<StudentCourseModel> StudentCourses { get; set; }
        public DbSet<TeacherCourseModel> TeacherCourses { get; set; }

    }
}
