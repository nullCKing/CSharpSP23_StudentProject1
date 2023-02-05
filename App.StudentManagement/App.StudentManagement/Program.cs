using System;
using App.StudentManagement.Helpers;
using Library.StudentManagement.Models;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var printMenu = new PrintHelper();
            var studentHelper = new StudentHelper();
            var courseHelper = new CourseHelper();
            printMenu.ConsolePrint();
            var input = Console.ReadLine();
            if(int.TryParse(input, out int result) ) 
            {
                while (result != 0)
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
                    else if (result == 5)
                    {
                        courseHelper.CreateCourse();
                    }
                    else if (result == 6)
                    {
                        courseHelper.ListAllCourses();
                    }
                    else if (result == 7)
                    {
                        courseHelper.SearchCourse();
                    }
                    else if (result == 8)
                    {
                        courseHelper.UpdateCourse();
                    }
                    printMenu.ConsolePrint();
                    input = Console.ReadLine();
                    int.TryParse(input, out result );
                }
            }   
        }
    }

}