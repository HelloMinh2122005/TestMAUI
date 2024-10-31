using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesStudentsMAUI.Models
{
    public class Student
    {
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public DateTime StudentDOB { get; set; }
        public string ClassID { get; set; }
        public Student() 
        {
            StudentID = "";
            StudentName = "";
            StudentDOB = new DateTime(0, 0, 0);
            ClassID = "";
        }

        public void Deconstruct(out string studentId, out string studentName, out DateTime studentDOB)
        {
            studentId = StudentID;
            studentName = StudentName;
            studentDOB = StudentDOB;
        }

    }
}
