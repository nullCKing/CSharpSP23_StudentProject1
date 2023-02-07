using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.StudentManagement.Models;

namespace Library.StudentManagement.Services
{
    public class AssignmentService
    {
        //private static object _lock = new object();
        private static AssignmentService? _instance;
        public static AssignmentService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssignmentService();
                }
                return _instance;
            }
        }

        public List<Assignment> assignmentList { get; set; }
        private AssignmentService()
        {
            assignmentList = new List<Assignment>();
        }

        public void Add(Assignment assignment)
        {
            assignmentList.Add(assignment);
        }

        public List<Assignment> Assignments
        {
            get { return assignmentList; }
        }

        public IEnumerable<Assignment> Search(string query)
        {
            //Can alternatively use List<Assignment> and add a .ToList() in place of IEnumerable, but this will create a deep copy.
            return assignmentList.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

    }
}
