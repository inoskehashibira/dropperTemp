using final;
using final.Entities;
using final.Relation;
using final.Relations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;


f_DbContext context = new f_DbContext();


string[] adminCredentials = { "admin", "101" };
string[] Users = { "student", "admin", "teachers" };

int choice = 1;

while (choice != 0)
{
    Console.WriteLine("Select Your Role\n1.Admin\n2.Teacher\n3.Student\nEnter 0 to quit...");
    choice = Int32.Parse(Console.ReadLine());


    switch (choice)
    {
        case 1:
            Admin();
            break;
        case 2:
            Teacher();
            break;
        case 3:
            Student();
            break;
        default:
            // code block
            break;
    }
}







void Admin()
{

    bool flag = LogIn(1);

    if (flag)
    {
        Console.WriteLine("Enter Your Choice:" +
            "\n1.Create Course" +
            "\n2.Create Teacher Role" +
            "\n3.Create Student Role" +
            "\n4.Assign Student to Course" +
            "\n5.Assign Teacher to Course" +
            "\n6.Set Class schedule for a course\n" +
            "Enter 0 to quit the menu");
        int choice = Int32.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:

                Console.WriteLine("Enter Course Title");
                string course_title = Console.ReadLine();
                Console.WriteLine("Enter Course Fees");
                double fees = double.Parse(Console.ReadLine());
                Course c1 = new Course { CourseName = course_title, Fees = fees };
                ContextManager(c1);
                break;

            case 2:
                Console.WriteLine("Enter Teacher Name");
                string Teacher_Name = Console.ReadLine();
                Console.WriteLine("Enter Teacher UserName");
                string Teacher_UserName = Console.ReadLine();
                Console.WriteLine("Enter Teacher Password");
                int Teacher_pass = Int32.Parse(Console.ReadLine());
                Teacher t1 = new Teacher { Name = Teacher_Name, userName = Teacher_UserName, Password = Teacher_pass };
                ContextManager(t1);
                break;


            case 3:
                Console.WriteLine("Enter Student Name");
                string Student_Name = Console.ReadLine();
                Console.WriteLine("Enter Student UserName");
                string Student_UserName = Console.ReadLine();
                Console.WriteLine("Enter Student Password");
                int Student_pass = Int32.Parse(Console.ReadLine());
                Student s1 = new Student { Name = Student_Name, userName = Student_UserName, Password = Student_pass };
                ContextManager(s1);
                break;


            case 4:

                Console.WriteLine("Enter Course Title");
                string c_title2 = Console.ReadLine();
                Console.WriteLine("Enter Student UserName");
                string S_UserName = Console.ReadLine();

                Course c11 = context.courses.Where(x => x.CourseName == c_title2).FirstOrDefault();
                Student s = context.students.Where(x => x.userName == S_UserName).FirstOrDefault();

                c11.RegisteredStudents = new List<Registration>();
                c11.RegisteredStudents.Add(new Registration { student = s });

                context.SaveChanges();
                break;

            case 5:

                Console.WriteLine("Enter Course Title");
                string c_title = Console.ReadLine();
                Console.WriteLine("Enter Teacher UserName");
                string T_UserName = Console.ReadLine();

                Course c = context.courses.Where(x => x.CourseName == c_title).FirstOrDefault();
                Teacher t = context.teachers.Where(x => x.userName == T_UserName).FirstOrDefault();

                c.AssignedTeachers = new List<AssignedCourse>();
                c.AssignedTeachers.Add(new AssignedCourse { teacher = t });

                context.SaveChanges();

                break;
            case 6:
                Admin();
                break;
            default:
                // code block
                break;
        }



    }

    space();
}




void ContextManager(object obj)
{
    context.Add(obj);

    if (context.SaveChanges() > 0)
    {
        Console.WriteLine("Database Updated Succesfully");
    }
    else
    {
        Console.WriteLine("Database updation Failed");
    }

}











void Teacher()
{


    Console.WriteLine("Enter UserName: ");
    string userName = Console.ReadLine();
    Console.WriteLine("Enter Password");
    int password = Int32.Parse(Console.ReadLine());


    Teacher t = context.teachers.Where(x => x.userName == userName).FirstOrDefault();

    bool flag = false;
    int index = 0;
    Dictionary<int, string> courseList = new Dictionary<int, string>();


    if (t.Password == password) flag = true;

    if (flag)
    {
        Console.WriteLine("Enter Your Choice:" +
            "\n1.Choose Course to check Attendance");



        Teacher t2 = context.teachers.Where(x => x.Id == t.Id).Include(x => x.AssignedCourses).FirstOrDefault();



        Console.WriteLine("Courses Assigned to " + t2.Name);
        foreach (var course in t2.AssignedCourses)
        {

            var tempCourse = context.courses.Where(x => x.Id == course.CourseID).FirstOrDefault();
            Console.WriteLine((index + 1) + tempCourse.CourseName);

            courseList[index] = tempCourse.CourseName;
            index++;

        }
        int choice = Int32.Parse(Console.ReadLine()) - 1;
        Console.WriteLine(courseList[choice]);



        Course tempCourse2 = context.courses.Where(x => x.CourseName == courseList[choice]).Include(x => x.StudentAttendance).FirstOrDefault();

        foreach (var course in tempCourse2.StudentAttendance)
        {


            Console.WriteLine("ID: " + course.StudentID.ToString() + " Course ID: " + course.CourseID + " Class Date: " + course.date + " Present status: " + course.present);

        }





    }

    space();
}

void Student()
{





    Console.WriteLine("Enter UserName: ");
    string userName = Console.ReadLine();
    Console.WriteLine("Enter Password");
    int password = Int32.Parse(Console.ReadLine());


    Student t = context.students.Where(x => x.userName == userName).FirstOrDefault();

    bool flag = false;
    int index = 0;

    Dictionary<int, string> courseList = new Dictionary<int, string>();


    if (t.Password == password) flag = true;

    if (flag)
    {
        Console.WriteLine("Choose Course Give Attendance:" +
            "\n List of Courses You are Enrolled In:");



        Student t2 = context.students.Where(x => x.Id == t.Id).Include(x => x.RegisteredCourses).FirstOrDefault();



        //Console.WriteLine("Courses Assigned to " + t2.Name);
        foreach (var course in t2.RegisteredCourses)
        {

            var tempCourse = context.courses.Where(x => x.Id == course.CourseID).FirstOrDefault();
            Console.WriteLine((index + 1) + ". " + tempCourse.CourseName);

            courseList[index] = tempCourse.CourseName;
            index++;

        }
        int choice = Int32.Parse(Console.ReadLine()) - 1;
        Console.WriteLine(courseList[choice]);

        Course tempCourse2 = context.courses.Where(x => x.CourseName == courseList[choice]).Include(x => x.StudentAttendance).FirstOrDefault();
        tempCourse2.StudentAttendance = new List<Attendance>();
        tempCourse2.StudentAttendance.Add(new Attendance { CourseID = tempCourse2.Id, StudentID = t.Id, date = DateTime.Now, present = true });

        context.SaveChanges();

        Console.WriteLine("you Attendance has been recorded!!");




        space();
    }

}

bool LogIn(int role)
{
    bool flag = false;

    Console.WriteLine("Enter UserName: ");
    string userName = Console.ReadLine();
    Console.WriteLine("Enter Password");
    string password = Console.ReadLine();

    if (userName == "1" && password == "1")
    {

        flag = true;

    }
    else
    {
        Console.WriteLine("Invalid Credentials\nTry Again");
    }

    return flag;
}


void space()
{
    Console.WriteLine("\n\n\n");
}