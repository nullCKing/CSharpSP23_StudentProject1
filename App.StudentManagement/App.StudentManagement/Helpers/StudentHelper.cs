using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;

namespace App.StudentManagement.Helpers
{
    internal class StudentHelper
    {
        private StudentService studentService = new StudentService();  
        public void CreateStudent()
        {

            Console.WriteLine("What is the name of the student? ");
            var name = Console.ReadLine();
            Console.WriteLine("What is the ID of the studet?");
            var id = Console.ReadLine();
            Console.WriteLine("What is the sutdent's classifcation? 'Freshman', 'Sophomore', 'Junior', 'Senior'");
            var classification = Console.ReadLine() ?? string.Empty;
            /*
            PersonClassification classEnum = PersonClassification.Freshman;

            if(classification.Equals("S", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = PersonClassification.Senior;
            }
            else if (classification.Equals("O", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = PersonClassification.Sophomore;
            }
            else if (classification.Equals("J", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = PersonClassification.Junior;
            }
            */

            var student = new Person
            {
                Id = int.Parse(id ?? "0"),
                Name = name ?? string.Empty,
                Classification = classification
            };

            studentService.Add(student);
            studentService.studentList.ForEach(Console.WriteLine); //student is being implied within ForEach
        }
    }
}
