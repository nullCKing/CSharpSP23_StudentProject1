using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;

namespace App.StudentManagement.Helpers
{
    public class CourseHelper
    {
        private CourseService courseService;
        private StudentService studentService;

        public CourseHelper() 
        {
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }

        public void CreateCourse(Course? selectedCourse = null)
        {
            bool isNewCourse = false;
            if (selectedCourse == null)
            {
                isNewCourse = true;
                selectedCourse = new Course();
            }

            var choice = "Y";
            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to update the course code?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                bool uniqueCode = true;
                while (uniqueCode)
                {
                    Console.WriteLine("What is the code of the course?");
                    var inputCode = Console.ReadLine() ?? string.Empty;
                    if (courseService.Courses.Any(c => c.Code.Equals(inputCode, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        Console.WriteLine($"Course code '{inputCode}' is already in use. Please choose a different code.");
                    }
                    else 
                    { 
                        uniqueCode = false;
                        selectedCourse.Code = inputCode;
                    }
                }
            }

            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to update the course name?");
                choice = Console.ReadLine() ?? "N";
            }
            else
            {
                choice = "Y";
            }
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What is the name of the course?");
                selectedCourse.Name = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to update the course description?");
                choice = Console.ReadLine() ?? "N";
            }
            else
            {
                choice = "Y";
            }
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What is the description of the course?");
                selectedCourse.Description = Console.ReadLine() ?? string.Empty;
            }

            if (isNewCourse)
            {

                SetupRoster(selectedCourse);
                SetupAssignments(selectedCourse);
            }


            if (isNewCourse)
            {
                courseService.Add(selectedCourse);
            }

        }

        public void UpdateCourse()
        {
            Console.WriteLine("Enter the code for the course to update:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(c => c.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                CreateCourse(selectedCourse);
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

            foreach (var course in courseService.Search(query))
            {
                Console.WriteLine(course);
                Console.WriteLine("Roster:");
                foreach (var person in course.Roster)
                {
                    Console.WriteLine("  " + person);
                }
                Console.WriteLine("Assignment:");
                foreach (var assignment in course.Assignments)
                {
                    Console.WriteLine("  " + assignment);
                }
            }
        }

        public void RemoveStudent()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Please enter the code for the course from which you'd like to remove a student (numeric values only):");

            var courseSelection = Console.ReadLine();

            if (int.TryParse(courseSelection, out int courseSelectionInt))
            {
                var selectedCourse = courseService.Courses.FirstOrDefault(c => c.Code == courseSelectionInt.ToString());
                if (selectedCourse != null)
                {
                    Console.WriteLine("Now listing all students enrolled in this course:");
                    selectedCourse.Roster.ForEach(Console.WriteLine);
                    Console.WriteLine("Please enter the ID of the student you'd like to remove (numeric values only):");

                    var studentSelection = Console.ReadLine();

                    if (int.TryParse(studentSelection, out int studentSelectionInt))
                    {
                        var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == studentSelectionInt);
                        if (selectedStudent != null)
                        {
                            selectedCourse.Roster.Remove(selectedStudent);
                            Console.WriteLine("Student successfully removed from the course.");
                        }
                        else
                        {
                            Console.WriteLine("Student not found.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Course not found.");
                }
            }
        }

        private void SetupRoster(Course c)
        {
            Console.WriteLine("Which students should be enrolled in this course? ('Q' to quit)");
            bool continueAdding = true;
            while (continueAdding)
            {
                studentService.Students.Where(s => !c.Roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                var selection = "Q";
                if (studentService.Students.Any(s => !c.Roster.Any(s2 => s2.Id == s.Id)))
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }

                if (selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    continueAdding = false;
                }
                else
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedId);

                    if (selectedStudent != null)
                    {
                        c.Roster.Add(selectedStudent);
                    }
                }
            }
        }

        private void SetupAssignments(Course c)
        {
            Console.WriteLine("Would you like to add assignments? (Y/N)");
            var assignResponse = Console.ReadLine() ?? "N";
            bool continueAdding;
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    c.Assignments.Add(CreateAssignment());
                    Console.WriteLine("Add more assignments? (Y/N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }

        }

        private Assignment CreateAssignment()
        {
            //Name
            Console.WriteLine("Name:");
            var assignmentName = Console.ReadLine() ?? string.Empty;
            //Description
            Console.WriteLine("Description:");
            var assignmentDescription = Console.ReadLine() ?? string.Empty;
            //TotalPoints
            Console.WriteLine("TotalPoints:");
            var totalPoints = int.Parse(Console.ReadLine() ?? "100");
            //DueDate
            Console.WriteLine("DueDate:");
            var dueDate = DateTime.Parse(Console.ReadLine() ?? "01/01/1900");

            return new Assignment
            {
                Name = assignmentName,
                Description = assignmentDescription,
                TotalAvailablePoints = totalPoints,
                DueDate = dueDate
            };
        }
    }
}

