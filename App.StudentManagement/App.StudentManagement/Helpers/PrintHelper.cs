using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.StudentManagement.Helpers
{
    public class PrintHelper
    {
        public void ConsolePrint()
        {
            Console.WriteLine("");
            Console.WriteLine("[0] Exit");
            Console.WriteLine("[1] Add student to registry");
            Console.WriteLine("[2] List all students");
            Console.WriteLine("[3] Search for a student");
            Console.WriteLine("[4] Update a student");
            Console.WriteLine("[5] Add a course");
            Console.WriteLine("[6] List all courses");
            Console.WriteLine("[7] Search for a course");
            Console.WriteLine("[8] Update a course");
            Console.WriteLine("[9] Remove a student from a course");
            Console.WriteLine("");
            Console.WriteLine("Enter option number:");
        }

    }
}
