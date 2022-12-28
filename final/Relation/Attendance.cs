﻿using final.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.Relations
{
    public class Attendance
    {
        public DateTime date { get; set; }
        public bool present { get; set; }

        public int StudentID { get; set; }
        public Student student { get; set; }



        public Course course { get; set; }
        public int CourseID { get; set; }
    }
}
