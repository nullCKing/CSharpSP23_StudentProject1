using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.StudentManagement.Models;

namespace Library.StudentManagement.Services
{
    public class CourseService
    {
        public List<Course> courseList = new List<Course>();

        private static CourseService? _instance;

        public static CourseService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CourseService();
                }

                return _instance;
            }
        }

        private CourseService()
        {
            courseList = new List<Course>();
        }

        public void Add(Course course)
        {
            courseList.Add(course);
        }

        public List<Course> Courses
        {
            get { return courseList; }
        }

        public IEnumerable<Course> Search(string query)
        {
            //Can alternatively use List<Person> and add a .ToList() in place of IEnumerable, but this will create a deep copy.
            return courseList.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

    }
}
