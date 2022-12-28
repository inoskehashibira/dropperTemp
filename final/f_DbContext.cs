using final.Entities;
using final.Relation;
using final.Relations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace final
{
    public class f_DbContext : DbContext
    {
        private readonly string _ConnectionString;
        private readonly string _migrationAssembly;

        public f_DbContext()
        {
            _ConnectionString = @"Server=DESKTOP-8VMMQPN\SQLEXPRESS;Database=final;Trusted_Connection = True;";
            _migrationAssembly = Assembly.GetExecutingAssembly().GetName().Name;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_ConnectionString, (x) =>
                {
                    x.MigrationsAssembly(_migrationAssembly);

                });
            }
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Course>().ToTable("AssignedCourses2");
            //modelBuilder.Entity<CourseRegistration>().ToTable("CourseRegistrations");


            //modelBuilder.Entity<Course>()
            //.HasOne<Teacher>(s => s.Teacher)
            //.WithMany(g => g.AssignedCourses)
            //.HasForeignKey(s => s.TeacherID);



            modelBuilder.Entity<Registration>().HasKey((x) => new { x.CourseID, x.StudentID });

            // key of courseRegi table
            modelBuilder.Entity<AssignedCourse>().HasKey((x) => new { x.CourseID, x.TeacherID });
            modelBuilder.Entity<Attendance>().HasKey((x) => new { x.CourseID, x.StudentID, x.date });


            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Course> courses { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }




    }
}


//dotnet ef database update --project final --context f_DbContext



//dotnet ef migrations add AddRegistration --project final --context f_DbContext

//dotnet ef migrations add Addusers --project final --context f_DbContext

//dotnet ef migrations add AddAssignedCourses --project final --context f_DbContext

//dotnet ef migrations add AddAttendance --project final --context f_DbContext




//dotnet ef migrations remove --context f_DbContext