using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.StudentManagement.Models;

namespace Library.StudentManagement.Services
{
    public class StudentService
    {
        public List<Person> studentList = new List<Person>();

        public void Add(Person person)
        {
            studentList.Add(person);
        }

        public List<Person> Students
        {
            get { return studentList; }
        }

    }
}
