using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.StudentManagement.Models
{
    public class Student : Person
    {
        public Dictionary<int, double> Grades { get; set; }

        public StudentClassification Classification { get; set; }

        public float ? GPA { get; set; }

        public Student() 
        {
            Grades = new Dictionary<int, double>();
        }

        public override string ToString()
        {
            return $"[#{Id}]: {Name} {Classification}";
        }
    }

    public enum StudentClassification
    {
        Freshman, Sophomore, Junior, Senior
    }
}
