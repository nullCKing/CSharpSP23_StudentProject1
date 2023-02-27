using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;

namespace App.StudentManagement.Helpers
{
    internal class StudentHelper
    {

        private StudentService studentService;
        private CourseService courseService;
        public StudentHelper()
        {
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }


        public List<Student> Students
        {
            get
            {
                return studentService.Students.ToList();
            }
        }
        
        public void CreateStudent(Student? selectedStudent = null)
        {

            Console.WriteLine("Enter student's name:");
            var name = Console.ReadLine();

            Console.WriteLine("Enter student year/classification: [1] Freshman | [2] Sophomore | [3] Junior | [4] Senior");
            var classification = Console.ReadLine() ?? string.Empty;
            int classificationInt = 1;

            while (!int.TryParse(classification, out classificationInt) || (classificationInt > 4) || (classificationInt < 1))
            {
                Console.WriteLine("Please re-enter student year/classification using only numeric values within the range of 1-4:");
                classification = Console.ReadLine() ?? string.Empty;
            }

            if (classificationInt == 2)
            {
                classification = "Sophomore";
            }
            else if(classificationInt == 3)
            {
                classification = "Junior";
            }
            else if(classificationInt == 4)
            {
                classification = "Senior";
            }
            else
            {
                classification = "Freshman";
            }

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

            bool isCreated = false;
            if (selectedStudent == null) {
                selectedStudent = new Student();
                isCreated= true;
            };

            selectedStudent.Name = name ?? string.Empty;
            selectedStudent.Classification = classification;

            if (isCreated)
            {
                selectedStudent.Id = assignedID;
                studentService.Add(selectedStudent);
            }
        }

        public void ListAllStudents()
        {
            studentService.Students.ForEach(Console.WriteLine);

            Console.WriteLine("Enter the ID of a specific student: ");
            var selection = Console.ReadLine();
            var selectionInt = int.Parse(selection ?? "0");

            Console.WriteLine("Student Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
        }

        public void SearchStudent()
        {
            Console.WriteLine("Enter a name to search for:");
            var query = Console.ReadLine() ?? string.Empty;

            studentService.Search(query).ToList().ForEach(Console.WriteLine);

            Console.WriteLine("Enter the ID of a specific student: ");
            var selection = Console.ReadLine();
            var selectionInt = int.Parse(selection ?? "0");


            Console.WriteLine("Student Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
        }

        public void UpdateStudent()
        {
            Console.WriteLine("Now listing all students:");
            studentService.Students.ForEach(Console.WriteLine);
            Console.WriteLine("Please enter the ID for the student you'd like to update (numeric values only):");

            var selection = Console.ReadLine();
            
            if (int.TryParse(selection, out int selectionInt))
            {
                var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectionInt);
                if(selectedStudent != null)
                {
                    CreateStudent(selectedStudent);
                }
            }

        }
    }
}
