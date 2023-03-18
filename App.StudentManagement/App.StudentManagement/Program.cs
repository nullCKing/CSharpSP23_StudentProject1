using System;
using App.StudentManagement.Helpers;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var personHelper = new PersonHelper();
            var courseHelper = new CourseHelper();
            bool continueMenu = true;
            while (continueMenu)
            {
                var displayResult = PrintHelper.ConsolePrint();
                if (displayResult == 1)
                {
                    PrintHelper.PersonPrint();
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int result))
                    {
                        if (result == 1)
                        {
                            personHelper.CreatePerson();
                        }
                        else if (result == 2)
                        {
                            personHelper.ListAllPersons();
                        }
                        else if (result == 3)
                        {
                            personHelper.SearchPerson();
                        }
                        else if (result == 4)
                        {
                            personHelper.UpdatePerson();
                        }
                    }
                }
                else if (displayResult == 2)
                {
                    PrintHelper.CoursePrint();
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int result))
                    {
                        if (result == 1)
                        {
                            courseHelper.CreateCourse();
                        }
                        else if (result == 2)
                        {
                            courseHelper.ListAllCourses();
                        }
                        else if (result == 3)
                        {
                            courseHelper.SearchCourse();
                        }
                        else if (result == 4)
                        {
                            courseHelper.UpdateCourse();
                        }
                        else if (result == 5)
                        {
                            courseHelper.AddPerson();
                        }
                        else if (result == 6)
                        {
                            courseHelper.RemovePerson();
                        }
                        else if (result == 7)
                        {
                            courseHelper.AddAssignment();
                        }
                        else if (result == 8)
                        {
                            courseHelper.UpdateAssignment();
                        }
                        else if (result == 9)
                        {
                            courseHelper.RemoveAssignment();
                        }
                        else if (result == 10)
                        {
                            courseHelper.AddModule();
                        }
                        else if (result == 11)
                        {
                            courseHelper.UpdateModule();
                        }
                        else if (result == 12)
                        {
                            courseHelper.RemoveModule();
                        }
                        else if (result == 13)
                        {
                            courseHelper.AddAnouncement();
                        }
                        else if (result == 14)
                        {
                            courseHelper.UpdateAnnouncement();
                        }
                        else if (result == 15)
                        {
                            courseHelper.RemoveAnnouncement();
                        }
                    }
                }
                else
                {
                    continueMenu = false;
                }
            }
        }
    }

}