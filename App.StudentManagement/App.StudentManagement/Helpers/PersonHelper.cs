using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;
using MyApp;

namespace App.StudentManagement.Helpers
{
    internal class PersonHelper
    {

        private PersonService personService;
        private CourseService courseService;
        private ListNavigator<Person> personNavigator;
        public PersonHelper()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
            personNavigator = new ListNavigator<Person>(personService.Persons, 2);
        }

        public List<Person> Persons
        {
            get
            {
                return personService.Persons.ToList();
            }
        }
        
        public void CreatePerson(Person? selectedPerson = null)
        {
            bool isCreated = false;
            if (selectedPerson == null)
            {
                isCreated = true;
                Console.WriteLine("Would you like to add: [1] Student | [2] Teaching Assistant | [3] Instructor");
                var personSelection = Console.ReadLine() ?? string.Empty;
                int personInt = 1;

                while (!int.TryParse(personSelection, out personInt) || (personInt > 4) || (personInt < 1))
                {
                    Console.WriteLine("Please re-enter year/classification using only numeric values within the range of 1-4:");
                    personSelection = Console.ReadLine() ?? string.Empty;
                }
                personInt = int.Parse(personSelection);

                if (personInt == 2)
                {
                    selectedPerson = new TeachingAssistant();
                }
                else if (personInt == 3)
                {
                    selectedPerson = new Instructor();
                }
                else
                {
                    selectedPerson = new Student();
                }
            };

            Console.WriteLine("Enter the person's name:");
            var name = Console.ReadLine();

            if (selectedPerson is Student)
            {
                Console.WriteLine("Enter person year/classification: [1] Freshman | [2] Sophomore | [3] Junior | [4] Senior");
                var classification = Console.ReadLine() ?? string.Empty;
                int classificationInt = 1;

                while (!int.TryParse(classification, out classificationInt) || (classificationInt > 4) || (classificationInt < 1))
                {
                    Console.WriteLine("Please re-enter student year/classification using only numeric values within the range of 1-4:");
                    classification = Console.ReadLine() ?? string.Empty;
                }

                classificationInt = int.Parse(classification);

                if (classificationInt == 2)
                {
                    classification = "Sophomore";
                }
                else if (classificationInt == 3)
                {
                    classification = "Junior";
                }
                else if (classificationInt == 4)
                {
                    classification = "Senior";
                }
                else
                {
                    classification = "Freshman";
                }

                var studentRecord = selectedPerson as Student;
                studentRecord.Classification = classification;
            }

            selectedPerson.Name = name ?? string.Empty;

            if (isCreated)
            {
                personService.Add(selectedPerson);
            }
        }

        public void UpdatePerson()
        {
            Console.WriteLine("Now listing all persons:");
            personService.Persons.ForEach(Console.WriteLine);
            Console.WriteLine("Please enter the ID for the person you'd like to update (numeric values only):");

            var selection = Console.ReadLine();

            if (int.TryParse(selection, out int selectionInt))
            {
                var selectedPerson = personService.Persons.FirstOrDefault(s => s.Id == selectionInt);
                if (selectedPerson != null)
                {
                    CreatePerson(selectedPerson);
                }
            }

        }

        private void NavigateStudents(string? query = null)
        {
            ListNavigator<Person>? currentNavigator = null;
            if (query == null)
            {
                currentNavigator = personNavigator;
            }
            else
            {
                currentNavigator = new ListNavigator<Person>(personService.Search(query).ToList(), 2);
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

                    Console.WriteLine("Student Course List:");
                    courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
                    keepPaging = false;
                }
            }
        }

        public void ListStudents()
        {
            NavigateStudents();
        }

        public void SearchStudents()
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;
            NavigateStudents(query);
        }

    }
}
