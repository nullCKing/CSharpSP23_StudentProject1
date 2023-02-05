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
        public void CreateCourse()
        {

            Console.WriteLine("Enter the course name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter the course description");
            var classification = Console.ReadLine() ?? string.Empty;

            Random random = new Random();
            int minValue = 100;
            int maxValue = 999;
            int assignedID = 0;

            while (true)
            {
                int randomId = random.Next(minValue, maxValue);
                if (!courseService.Courses.Any(c => c.Code == randomId.ToString()))
                {
                    assignedID = randomId;
                    break;
                }
            }

            var course = new Course
            {
                Code = assignedID.ToString(),
                Name = name ?? string.Empty,
                Description = classification
            };

            courseService.Add(course);
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

    }
}

