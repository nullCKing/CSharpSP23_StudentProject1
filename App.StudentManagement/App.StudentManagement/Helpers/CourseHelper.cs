using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;
using MyApp;

namespace App.StudentManagement.Helpers
{
    public class CourseHelper
    {
        private CourseService courseService;
        private PersonService personService;
        private ListNavigator<Course> courseNavigator;

        public CourseHelper() 
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
            courseNavigator = new ListNavigator<Course>(courseService.Courses, 2);
        }

        private void NavigateCourses(string? query = null)
        {
            ListNavigator<Course>? currentNavigator = null;
            if (query == null)
            {
                currentNavigator = courseNavigator;
            }
            else
            {
                currentNavigator = new ListNavigator<Course>(courseService.Search(query).ToList(), 2);
            }

            bool keepPaging = true;
            while (keepPaging)
            {
                foreach (var pair in currentNavigator.GetCurrentPage())
                {
                    Console.WriteLine($"{pair.Key}. {pair.Value}");
                }

                if (currentNavigator.HasPreviousPage)
                {
                    Console.WriteLine("P. Previous Page");
                }

                if (currentNavigator.HasNextPage)
                {
                    Console.WriteLine("N. Next Page");
                }

                Console.WriteLine("Make a selection:");
                var selectionStr = Console.ReadLine();

                if ((selectionStr?.Equals("P", StringComparison.InvariantCultureIgnoreCase) ?? false)
                    || (selectionStr?.Equals("N", StringComparison.InvariantCultureIgnoreCase) ?? false))
                {
                    //Navigate through pages
                    if (selectionStr.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                    {
                        currentNavigator.GoBackward();
                    }
                    else if (selectionStr.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        currentNavigator.GoForward();
                    }
                }
                else
                {
                    var selectionInt = int.Parse(selectionStr ?? "0");
                    //Not really sure what should and shouldn't be displayed right here.
                    Console.WriteLine("Course Assignment List:");
                    courseService.Courses.Where(c => c.Assignments.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
                    Console.WriteLine("Course Modules List:");
                    courseService.Courses.Where(c => c.Modules.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
                        Console.WriteLine("Course Student List:");
                    courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
                    keepPaging = false;
                }
            }
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

            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to update the course credit hours?");
                choice = Console.ReadLine() ?? "N";
            }
            else
            {
                choice = "Y";
            }
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Enter the credit hours for the course:");
                if (int.TryParse(Console.ReadLine(), out int creditHours))
                {
                    selectedCourse.CreditHours = creditHours;
                }
                else
                {
                    selectedCourse.CreditHours = 0;
                }
            }

            if (isNewCourse)
            {

                SetupRoster(selectedCourse);
                SetupAssignments(selectedCourse);
                SetupModules(selectedCourse);
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
            NavigateCourses();
        }

        public void SearchCourse()
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;
            NavigateCourses(query);
        }

        public void CalculateWeightedGrades()
        {
            Console.WriteLine("Enter the code for the course to list the grade roster of:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                /*
                foreach (var student in selectedCourse.Roster.OfType<Student>())
                {
                    double totalGrade = 0;
                    double totalWeight = 0;

                    foreach (var group in selectedCourse.AssignmentGroups)
                    {
                        double groupGrade = 0;
                        double groupWeight = (group.CourseWeight ?? 0) / 100.0;
                        int groupTotalPoints = 0;

                        foreach (var assignment in group.GroupedAssignments)
                        {
                            if (student.Grades.TryGetValue(assignment.Id, out var grade))
                            {
                                groupGrade += grade / assignment.TotalAvailablePoints;
                                groupTotalPoints += assignment.TotalAvailablePoints;
                            }
                        }

                        if (groupTotalPoints > 0)
                        {
                            totalGrade += groupGrade / groupTotalPoints * groupWeight;
                            totalWeight += groupWeight;
                        }
                    }

                    if (totalWeight > 0)
                    {
                        double weightedGrade = totalGrade / totalWeight * 100;
                        selectedCourse.GradeRoster[student] = weightedGrade; // add or update the student's weighted grade in the grade roster dictionary
                    }
                }

                foreach (var student in selectedCourse.GradeRoster)
                {
                    Console.WriteLine($"{student.Key}: {student.Value}");
                }
                */
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

        public void AddAnouncement()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the code for the course:");
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Announcements.Add(CreateAnnouncement());
            }
        }
        public void UpdateAnnouncement()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the code for the course:");
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an announcement to update:");
                selectedCourse.Announcements.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAnnouncement = selectedCourse.Announcements.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAnnouncement != null)
                {
                    var index = selectedCourse.Announcements.IndexOf(selectedAnnouncement);
                    selectedCourse.Announcements.RemoveAt(index);
                    selectedCourse.Announcements.Insert(index, CreateAnnouncement());
                }
            }
        }
        public void RemoveAnnouncement()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the code for the course:");
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an announcement to delete:");
                selectedCourse.Announcements.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAnnouncement = selectedCourse.Announcements.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAnnouncement != null)
                {
                    selectedCourse.Announcements.Remove(selectedAnnouncement);
                }
            }
        }
            
        public void AddAssignment()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Please enter the code for the course which you'd like to add an assignment to:");
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
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the code for the course:");
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

        public void AddModule()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the code for the course:");
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Modules.Add(CreateModule(selectedCourse));
            }
        }

        public void RemoveAssignmentGroup()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the code for the course:");
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Enter the name of an assignment group to delete:");
                selectedCourse.AssignmentGroups.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectedModule = selectedCourse.AssignmentGroups.FirstOrDefault(a => a.Name == selectionStr);
                if (selectedModule != null)
                {
                    selectedCourse.AssignmentGroups.Remove(selectedModule);
                }
            }
        }

        public void AddAssignmentGroup()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the code for the course:");
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.AssignmentGroups.Add(CreateAssignmentGroup(selectedCourse));
            }
        }

        public void UpdateModule()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the code for the course:");
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));

            if (selectedCourse != null && selectedCourse.Modules.Any())
            {
                Console.WriteLine("Now listing all modules:");
                selectedCourse.Modules.ForEach(Console.WriteLine);
                Console.WriteLine("Enter the id for the module:");

                selection = Console.ReadLine();
                var selectedModule = selectedCourse.Modules.FirstOrDefault(m => m.Id.ToString().Equals(selection, StringComparison.InvariantCultureIgnoreCase));

                if (selectedModule != null)
                {
                    Console.WriteLine("Would you like to modify the module name?");
                    selection = Console.ReadLine();
                    if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        Console.WriteLine("Name:");
                        selectedModule.Name = Console.ReadLine();
                    }
                    Console.WriteLine("Would you like to modify the module description?");
                    selection = Console.ReadLine();
                    if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        Console.WriteLine("Description:");
                        selectedModule.Description = Console.ReadLine();
                    }

                    Console.WriteLine("Would you like to delete content from this module?");
                    selection = Console.ReadLine();
                    if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        var keepRemoving = true;
                        while (keepRemoving)
                        {
                            selectedModule.Content.ForEach(Console.WriteLine);
                            selection = Console.ReadLine();

                            var contentToRemove = selectedModule
                                .Content
                                .FirstOrDefault(c => c.Id.ToString().Equals(selection, StringComparison.InvariantCultureIgnoreCase));
                            if (contentToRemove != null)
                            {
                                selectedModule.Content.Remove(contentToRemove);
                            }

                            Console.WriteLine("Would you like to remove more content?");
                            selection = Console.ReadLine();
                            if (selection?.Equals("N", StringComparison.InvariantCultureIgnoreCase) ?? false)
                            {
                                keepRemoving = false;
                            }
                        }

                    }

                    Console.WriteLine("Would you like to add content?");
                    var choice = Console.ReadLine() ?? "N";
                    while (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("What type of content would you like to add?");
                        Console.WriteLine("1. Assignment");
                        Console.WriteLine("2. File");
                        Console.WriteLine("3. Page");
                        var contentChoice = int.Parse(Console.ReadLine() ?? "0");

                        switch (contentChoice)
                        {
                            case 1:
                                var newAssignmentContent = CreateAssignmentItem(selectedCourse);
                                if (newAssignmentContent != null)
                                {
                                    selectedModule.Content.Add(newAssignmentContent);
                                }
                                break;
                            case 2:
                                var newFileContent = CreateFileItem(selectedCourse);
                                if (newFileContent != null)
                                {
                                    selectedModule.Content.Add(newFileContent);
                                }
                                break;
                            case 3:
                                var newPageContent = CreatePageItem(selectedCourse);
                                if (newPageContent != null)
                                {
                                    selectedModule.Content.Add(newPageContent);
                                }
                                break;
                            default:
                                break;
                        }

                        Console.WriteLine("Would you like to add more content?");
                        choice = Console.ReadLine() ?? "N";
                    }

                }

            }

        }

        public void RemoveModule()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the code for the course:");
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose a module to delete:");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedModule = selectedCourse.Modules.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedModule != null)
                {
                    selectedCourse.Modules.Remove(selectedModule);
                }
            }
        }
        public void AddGrade()
        {
            Console.WriteLine("Now listing all courses:");
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the code for the course:");
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Roster.ForEach(Console.WriteLine);
                Console.WriteLine("Enter the Id for the student:");
                var studentIdString = Console.ReadLine();
                var studentIdInt = int.Parse(studentIdString);

                Console.WriteLine("Enter the Id for the assignment:");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var assignmentIdString = Console.ReadLine() ?? string.Empty;
                var assignmentIdInt = int.Parse(assignmentIdString);

                Console.WriteLine("Enter the grade to be given for this assignment:");
                var gradeString = Console.ReadLine();
                var gradeInt = int.Parse(gradeString);

                AssignGrade(selectedCourse, studentIdInt, assignmentIdInt, gradeInt);
            }
        }

        private void AssignGrade(Course course, int studentId, int assignmentId, double grade)
        {
            
            // Find the student with the given ID
            Student student = course.Roster.OfType<Student>().FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                Console.WriteLine($"Error: Student with ID {studentId} not found.");
                return;
            }

            // Find the assignment with the given ID
            Assignment assignment = course.Assignments.FirstOrDefault(a => a.Id == assignmentId);
            if (assignment == null)
            {
                Console.WriteLine($"Error: Assignment with ID {assignmentId} not found.");
                return;
            }

            // Add or update the grade for the student and assignment
            if (student.Grades.ContainsKey(assignment.Id))
            {
                student.Grades[assignment.Id] = grade;
            }
            else
            {
                student.Grades.Add(assignment.Id, grade);
            }

            Console.WriteLine($"Grade of {grade} assigned to assignment {assignment.Name} for student {student.Name} ({student.Id}).");
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

        private Announcement CreateAnnouncement()
        {
            Console.WriteLine("Name:");
            var headline = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Description:");
            var internalText = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("DueDate:");
            var date = DateTime.Parse(Console.ReadLine() ?? "01/01/1900");

            return new Announcement
            {
                Headline = headline,
                InternalText = internalText,
                Date = date
            };
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

        private void SetupModules (Course course)
        {
            Console.WriteLine("Would you like to add modules? (Y/N)");
            var assignResponse = Console.ReadLine() ?? "N";
            bool continueAdding;
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    course.Modules.Add(CreateModule(course));
                    Console.WriteLine("Add more modules? (Y/N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }
        }

        private Module CreateModule(Course course)
        {
            //Name
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? string.Empty;
            //Description
            Console.WriteLine("Description:");
            var description = Console.ReadLine() ?? string.Empty;

            var module = new Module
            {
                Name = name,
                Description = description
            };
            Console.WriteLine("Would you like to add content?");
            var choice = Console.ReadLine() ?? "N";
            while (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What type of content would you like to add: [1] Assignment | [2] File | [3] Page");
                var contentChoice = int.Parse(Console.ReadLine() ?? "0");

                switch (contentChoice)
                {
                    case 1:
                        var newAssignmentContent = CreateAssignmentItem(course);
                        if (newAssignmentContent != null)
                        {
                            module.Content.Add(newAssignmentContent);
                        }
                        break;
                    case 2:
                        var newFileContent = CreateFileItem(course);
                        if (newFileContent != null)
                        {
                            module.Content.Add(newFileContent);
                        }
                        break;
                    case 3:
                        var newPageContent = CreatePageItem(course);
                        if (newPageContent != null)
                        {
                            module.Content.Add(newPageContent);
                        }
                        break;
                    default:
                        break;
                }

                Console.WriteLine("Would you like to add more content?");
                choice = Console.ReadLine() ?? "N";
            }
            return module;
        }

        private AssignmentGroup CreateAssignmentGroup(Course course)
        {
            var assignmentGroup = new AssignmentGroup();

            Console.WriteLine("Name:");
            assignmentGroup.Name = Console.ReadLine() ?? string.Empty;
            //Description
            Console.WriteLine("Course weight percentage (ex: 50.00):");
            if (float.TryParse(Console.ReadLine(), out float courseWeight))
            {
                assignmentGroup.CourseWeight = courseWeight;
            }

            Console.WriteLine("Now add assignments to group:");
            Console.WriteLine("Full assignment list:");
            foreach (var assignment in course.Assignments)
            {
                Console.WriteLine("  " + assignment);
            }

            // Prompt the user for the assignment IDs to add to the assignment group
            Console.WriteLine("Enter the IDs of the assignments to add (comma separated):");
            string input = Console.ReadLine() ?? string.Empty;

            course.AssignmentGroups.Add(assignmentGroup);

            // Add the selected assignments to the new assignment group
            foreach (string id in input.Split(','))
            {
                if (int.TryParse(id, out int assignmentId))
                {
                    Assignment assignment = course.Assignments.FirstOrDefault(a => a.Id == assignmentId);
                    if (assignment != null)
                    {
                        assignmentGroup.GroupedAssignments.Add(assignment);
                    }
                }
            }

            // Prompt the user if they want to add more assignments
            Console.WriteLine("Would you like to add more assignments?");
            return assignmentGroup;
        }

        private AssignmentItem? CreateAssignmentItem(Course course)
        {
            Console.WriteLine("Now listing all assignments:");
            course.Assignments.ForEach(Console.WriteLine);
            Console.WriteLine("Enter the ID for which assignment you'd like to add:");
            var choice = int.Parse(Console.ReadLine() ?? "-1");
            if (choice >= 0)
            {
                var assignment = course.Assignments.FirstOrDefault(c => c.Id == choice);
                return new AssignmentItem()
                {
                    Assignment = assignment
                };
            }
            return null;
        }

        private FileItem? CreateFileItem(Course c)
        {
            //Name
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? string.Empty;
            //Description
            Console.WriteLine("Description:");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter a path to the file:");
            var filepath = Console.ReadLine();

            return new FileItem
            {
                Name = name,
                Description = description,
                Path = filepath
            };
        }

        private PageItem? CreatePageItem(Course c)
        {
            //Name
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? string.Empty;
            //Description
            Console.WriteLine("Description:");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter page content:");
            var body = Console.ReadLine();

            return new PageItem
            {
                Name = name,
                Description = description,
                HTMLBody = body
            };
        }
    }
}

