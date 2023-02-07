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
        private List<Person> studentList;

        private static StudentService? _instance;

        private StudentService()
        {
            studentList = new List<Person>();
        }

        public static StudentService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudentService();
                }
                return _instance;
            }
        }


        public void Add(Person person)
        {
            studentList.Add(person);
        }

        public List<Person> Students
        {
            get { return studentList; }
        }

        public IEnumerable<Person> Search(string query)
        {
            //Can alternatively use List<Person> and add a .ToList() in place of IEnumerable, but this will create a deep copy.
            return studentList.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

    }
}
