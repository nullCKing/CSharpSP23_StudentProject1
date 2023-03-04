using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.StudentManagement.Helpers
{
    public class PrintHelper
    {
        public static int ConsolePrint()
        {
            Console.WriteLine("");
            Console.WriteLine("[0] Exit");
            Console.WriteLine("[1] Manage student data");
            Console.WriteLine("[2] Manage course data");
            Console.WriteLine("");
            Console.WriteLine("Enter option number:");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if ((result <= 2) && (result >= 0))
                {
                    return result;
                }
            }
            return 1;   //Default to student data for now if user enters something unexpected.
        }

        public static void StudentPrint()
        {
            Console.WriteLine("[1] Add student to registry");
            Console.WriteLine("[2] List all students");
            Console.WriteLine("[3] Search for a student");
            Console.WriteLine("[4] Update a student");
            Console.WriteLine("Enter option number:");
        }

        public static void CoursePrint()
        {
            Console.WriteLine("[1] Add a course");
            Console.WriteLine("[2] List all courses");
            Console.WriteLine("[3] Search for a course");
            Console.WriteLine("[4] Update a course");
            Console.WriteLine("[5] Add a student to a course");
            Console.WriteLine("[6] Remove a student from a course");
            Console.WriteLine("[7] Add an assignment to a course");
            Console.WriteLine("[8] Update an assignment");
            Console.WriteLine("[9] Remove an assignment from a course");
            Console.WriteLine("Enter option number:");
        }
    }
}
