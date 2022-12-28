using final.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string userName { get; set; }
        public int Password { get; set; }







        public List<Registration> RegisteredCourses { get; set; }//relation to courses.

        public List<Attendance> CourseAttendance { get; set; }//relation to courses





    }
}

