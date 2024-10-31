using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesStudentsMAUI.Models
{
    public class Class
    {
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public Class()
        {
            ClassID = "";
            ClassName = "";
        }
    }
}
