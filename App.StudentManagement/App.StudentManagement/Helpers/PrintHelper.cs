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
            Console.WriteLine("[1] Manage person data");
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
            return 1;   //Default to person data for now if user enters something unexpected.
        }

        public static void PersonPrint()
        {
            Console.WriteLine("[1] Add person to registry");
            Console.WriteLine("[2] List all persons");
            Console.WriteLine("[3] Search for a person");
            Console.WriteLine("[4] Update a person");
            Console.WriteLine("Enter option number:");
        }

        public static void CoursePrint()
        {
            Console.WriteLine("[1] Add a course");
            Console.WriteLine("[2] List all courses");
            Console.WriteLine("[3] Search for a course");
            Console.WriteLine("[4] Update a course");
            Console.WriteLine("[5] Add a person to a course");
            Console.WriteLine("[6] Remove a person from a course");
            Console.WriteLine("[7] Add an assignment to a course");
            Console.WriteLine("[8] Update an assignment");
            Console.WriteLine("[9] Remove an assignment from a course");
            Console.WriteLine("[10] Add module to a course");
            Console.WriteLine("[11] Update a course module");
            Console.WriteLine("[12] Remove a course module");
            Console.WriteLine("[13] Add an announcement");
            Console.WriteLine("[14] Update an announcement");
            Console.WriteLine("[15] Remove an announcement");
            Console.WriteLine("Enter option number:");
        }
    }
}
