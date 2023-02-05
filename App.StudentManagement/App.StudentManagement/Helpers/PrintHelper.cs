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
            Console.WriteLine("");
            Console.WriteLine("Enter option number:");
        }

    }
}
