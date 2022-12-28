using final.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.Relation
{
    public class AssignedCourse
    {

        public int CourseID { get; set; }
        public Course course { get; set; }



        public int TeacherID { get; set; }
        public Teacher teacher { get; set; }
    }
}
