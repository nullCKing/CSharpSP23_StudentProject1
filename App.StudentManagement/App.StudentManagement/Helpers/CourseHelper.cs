using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;

namespace App.StudentManagement.Helpers
{
    public class CourseHelper
    {
        private CourseService courseService = new CourseService();
        public void CreateCourse(Course? selectedCourse = null)
        {

            Console.WriteLine("Enter the course name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter the course description");
            var classification = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter the course code:");
            var courseCode = Console.ReadLine() ?? string.Empty;
            while (courseService.Courses.Any(c => c.Code == courseCode))
            {
                Console.WriteLine("Please re-enter a unique course code:");
                classification = Console.ReadLine() ?? string.Empty;
            }

            bool isCreated = false;
            if (selectedCourse == null)
            {
                selectedCourse = new Course();
                isCreated = true;
            };

            selectedCourse.Name = name ?? string.Empty;
            selectedCourse.Description = classification;
            selectedCourse.Code = courseCode;

            if (isCreated)
            {
                courseService.Add(selectedCourse);
            }

        }

        public void ListAllCourses()
        {
            courseService.Courses.ForEach(Console.WriteLine);
        }

        public void SearchCourse()
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            courseService.Search(query).ToList().ForEach(Console.WriteLine);
        }

        public void UpdateCourse()
        {
            Console.WriteLine("Now listing all course:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Please enter the code for the course you'd like to update (numeric values only):");

            var selection = Console.ReadLine();

            if (int.TryParse(selection, out int selectionInt))
            {
                var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code == selectionInt.ToString());
                if (selectedCourse != null)
                {
                    CreateCourse(selectedCourse);
                }
            }

        }

    }
}

