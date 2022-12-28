using final.Relation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string userName { get; set; }
        public int Password { get; set; }






        ////public int courseID { get; set; }
        //public List<Course> AssignedCourses { get; set; }

        public List<AssignedCourse> AssignedCourses { get; set; }


    }
}
