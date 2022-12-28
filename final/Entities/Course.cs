using final.Relation;
using final.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public Double Fees { get; set; }







        public List<Registration> RegisteredStudents { get; set; } //relation to students

        public List<AssignedCourse> AssignedTeachers { get; set; }

        //public int TeacherID { get; set; }
        //public Teacher Teacher { get; set; }//relation to teacher


        public List<Attendance> StudentAttendance { get; set; }//relation to students

    }
}
