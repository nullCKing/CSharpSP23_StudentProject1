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

            Console.WriteLine("Enter the course name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter the course description:");
            var classification = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter the course code:");
            var courseCode = Console.ReadLine() ?? string.Empty;
            while (courseService.Courses.Any(c => c.Code == courseCode))
            {
                Console.WriteLine("Please re-enter a unique course code:");
                classification = Console.ReadLine() ?? string.Empty;
            }

            var roster = new List<Person>();
            var assignments = new List<Assignment>();
            Console.WriteLine("Which students should be entrolled in this course? (Enter numeric ID || Q to exit)");
            bool contAdd = true;
            while (contAdd)
            {
                //Where clause keeps student from being duplicated in roster
                studentService.Students.ToList().Where(s => !roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                var selection = Console.ReadLine() ?? string.Empty;

                if (selection == "q" || selection == "Q")
                {
                    contAdd = false;
                }
                else
                {
                    var selectedID = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedID);

                    if (selectedStudent != null)
                    {
                        roster.Add(selectedStudent);
                    }
                }
            }

            bool isCreated = false;
            if (selectedCourse == null)
            {
                selectedCourse = new Course();
                selectedCourse.Assignments = new List<Assignment>();
                isCreated = true;
            };

            selectedCourse.Name = name ?? string.Empty;
            selectedCourse.Description = classification;
            selectedCourse.Code = courseCode;
            selectedCourse.Roster = new List<Person>();
            selectedCourse.Roster.AddRange(roster);

            bool assignmentAdd = true;
            selectedCourse.Assignments = new List<Assignment>();
            while (assignmentAdd)
            {
                Console.WriteLine("Create assignments for this course || (Enter Y to proceed or N to stop)");
                var selection = Console.ReadLine() ?? string.Empty;

                if (selection == "n" || selection == "N")
                {
                    assignmentAdd = false;
                }
                else
                {
                    Console.WriteLine("Enter total available points: ");
                    var points = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Enter assignment name: ");
                    var assignmentName = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Enter assignment description: ");
                    var description = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Enter a due date (MM/dd/yyyy hh:mm:ss): ");
                    string dueInput = Console.ReadLine();
                    DateTime due;
                    if (!DateTime.TryParseExact(dueInput, "MM/dd/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out due))
                    {
                        Console.WriteLine("Invalid date format.");
                    }
                    Assignment assignment = new Assignment();
                    assignment.Name = assignmentName;
                    assignment.Description = description;
                    assignment.DueDate = due;
                    if (int.TryParse(points, out int pointsInt))
                    {
                        assignment.TotalAvailablePoints = pointsInt;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                    }
                    selectedCourse.Assignments.Add(assignment);
                }
            }


            Console.WriteLine("Assignment:");
            foreach (var assignment in selectedCourse.Assignments)
            {
                Console.WriteLine("  " + assignment);
            }

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
    }
}

