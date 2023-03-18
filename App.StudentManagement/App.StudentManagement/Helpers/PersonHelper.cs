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
    internal class PersonHelper
    {

        private PersonService personService;
        private CourseService courseService;
        public PersonHelper()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
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

        public void ListAllPersons()
        {
            personService.Persons.ForEach(Console.WriteLine);

            Console.WriteLine("Enter the ID of a specific person: ");
            var selection = Console.ReadLine();
            var selectionInt = int.Parse(selection ?? "0");

            Console.WriteLine("Person Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
        }

        public void SearchPerson()
        {
            Console.WriteLine("Enter a name to search for:");
            var query = Console.ReadLine() ?? string.Empty;

            personService.Search(query).ToList().ForEach(Console.WriteLine);

            Console.WriteLine("Enter the ID of a specific person: ");
            var selection = Console.ReadLine();
            var selectionInt = int.Parse(selection ?? "0");


            Console.WriteLine("Person Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
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
                if(selectedPerson != null)
                {
                    CreatePerson(selectedPerson);
                }
            }

        }
    }
}
