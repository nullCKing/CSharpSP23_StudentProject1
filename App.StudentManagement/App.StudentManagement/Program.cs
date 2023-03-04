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
            var studentHelper = new StudentHelper();
            var courseHelper = new CourseHelper();
            bool continueMenu = true;
            while (continueMenu)
            {
                var displayResult = PrintHelper.ConsolePrint();
                if (displayResult == 1)
                {
                    PrintHelper.StudentPrint();
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int result))
                    {
                        if (result == 1)
                        {
                            studentHelper.CreateStudent();
                        }
                        else if (result == 2)
                        {
                            studentHelper.ListAllStudents();
                        }
                        else if (result == 3)
                        {
                            studentHelper.SearchStudent();
                        }
                        else if (result == 4)
                        {
                            studentHelper.UpdateStudent();
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
                            courseHelper.AddStudent();
                        }
                        else if (result == 6)
                        {
                            courseHelper.RemoveStudent();
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