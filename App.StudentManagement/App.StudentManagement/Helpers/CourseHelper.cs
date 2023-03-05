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
        private PersonService personService;

        public CourseHelper() 
        {
            personService = PersonService.Current;
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
            Console.WriteLine("Enter a course code or name:");
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

        public void AddPerson()
        {
            Console.WriteLine("Enter the code for the course to add the person to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                personService.Persons.Where(s => !selectedCourse.Roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                if (personService.Persons.Any(s => !selectedCourse.Roster.Any(s2 => s2.Id == s.Id)))
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }

                if (selection != null)
                {
                    var selectedId = int.Parse(selection);
                    var selectedPerson = personService.Persons.FirstOrDefault(s => s.Id == selectedId);
                    if (selectedPerson != null)
                    {
                        selectedCourse.Roster.Add(selectedPerson);
                    }
                }

            }
        }
        public void RemovePerson()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Please enter the code for the course from which you'd like to remove a person (numeric values only):");

            var courseSelection = Console.ReadLine();

            if (int.TryParse(courseSelection, out int courseSelectionInt))
            {
                var selectedCourse = courseService.Courses.FirstOrDefault(c => c.Code == courseSelectionInt.ToString());
                if (selectedCourse != null)
                {
                    Console.WriteLine("Now listing all persons enrolled in this course:");
                    selectedCourse.Roster.ForEach(Console.WriteLine);
                    Console.WriteLine("Please enter the ID of the person you'd like to remove (numeric values only):");

                    var personSelection = Console.ReadLine();

                    if (int.TryParse(personSelection, out int personSelectionInt))
                    {
                        var selectedPerson = personService.Persons.FirstOrDefault(s => s.Id == personSelectionInt);
                        if (selectedPerson != null)
                        {
                            selectedCourse.Roster.Remove(selectedPerson);
                            Console.WriteLine("Person successfully removed from the course.");
                        }
                        else
                        {
                            Console.WriteLine("Person not found.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Course not found.");
                }
            }
        }
        public void AddAssignment()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Please enter the code for the course which you'd like to add an assignment to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Assignments.Add(CreateAssignment());
            }
        }
        public void UpdateAssignment()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the code for the course:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an assignment to update:");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAssignment = selectedCourse.Assignments.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAssignment != null)
                {
                    var index = selectedCourse.Assignments.IndexOf(selectedAssignment);
                    selectedCourse.Assignments.RemoveAt(index);
                    selectedCourse.Assignments.Insert(index, CreateAssignment());
                }
            }
        }
        public void RemoveAssignment()
        {
            Console.WriteLine("Enter the code for the course:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an assignment to delete:");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAssignment = selectedCourse.Assignments.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAssignment != null)
                {
                    selectedCourse.Assignments.Remove(selectedAssignment);
                }
            }
        }
        private void SetupRoster(Course c)
        {
            Console.WriteLine("Which persons should be enrolled in this course? ('Q' to quit)");
            bool continueAdding = true;
            while (continueAdding)
            {
                personService.Persons.Where(s => !c.Roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                var selection = "Q";
                if (personService.Persons.Any(s => !c.Roster.Any(s2 => s2.Id == s.Id)))
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
                    var selectedPerson = personService.Persons.FirstOrDefault(s => s.Id == selectedId);

                    if (selectedPerson != null)
                    {
                        c.Roster.Add(selectedPerson);
                    }
                }
            }
        }

        private void SetupAssignments(Course c)
        {
            Console.WriteLine("Would you like to add assignments? (Y/N)");
            var assignResponse = Console.ReadLine() ?? "N";
            bool cont = true;
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                while (cont)
                {
                    c.Assignments.Add(CreateAssignment());
                    Console.WriteLine("Add more assignments? (Y/N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        cont = false;
                    }
                }
            }

        }

        private Assignment CreateAssignment()
        {
            Console.WriteLine("Name:");
            var assignmentName = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Description:");
            var assignmentDescription = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("TotalPoints:");
            var totalPoints = int.Parse(Console.ReadLine() ?? "100");
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

