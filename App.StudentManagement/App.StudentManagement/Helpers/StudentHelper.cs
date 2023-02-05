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
            Console.WriteLine("What is the sutdent's classifcation? 'Freshman', 'Sophomore', 'Junior', 'Senior'");
            var classification = Console.ReadLine() ?? string.Empty;

            Random random = new Random();
            int minValue = 1000;
            int maxValue = 9999;
            int assignedID = 0;

            while (true)
            {
                int randomId = random.Next(minValue, maxValue);
                if (!studentService.Students.Any(s => s.Id == randomId))
                {
                    assignedID = randomId;
                    break;
                }
            }

            var student = new Person
            {
                Id = assignedID,
                Name = name ?? string.Empty,
                Classification = classification
            };

            studentService.Add(student);
        }

        public void ListAllStudents()
        {
            studentService.Students.ForEach(Console.WriteLine);
        }

        public void SearchStudent()
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            studentService.Search(query).ToList().ForEach(Console.WriteLine);
        }

    }
}
