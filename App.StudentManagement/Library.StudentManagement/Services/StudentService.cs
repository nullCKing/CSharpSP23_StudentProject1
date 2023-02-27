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
        private List<Student> studentList;

        private static StudentService? _instance;

        private StudentService()
        {
            studentList = new List<Student>();
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


        public void Add(Student student)
        {
            studentList.Add(student);
        }

        public List<Student> Students
        {
            get { return studentList; }
        }

        public IEnumerable<Student> Search(string query)
        {
            //Can alternatively use List<Person> and add a .ToList() in place of IEnumerable, but this will create a deep copy.
            return studentList.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

    }
}
