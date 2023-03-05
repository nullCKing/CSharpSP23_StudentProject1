using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.StudentManagement.Models;

namespace Library.StudentManagement.Services
{
    public class PersonService
    {
        private List<Person> personList;

        private static PersonService? _instance;

        private PersonService()
        {
            personList = new List<Person>();
        }

        public static PersonService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PersonService();
                }
                return _instance;
            }
        }


        public void Add(Person person)
        {
            personList.Add(person);
        }

        public List<Person> Persons
        {
            get { return personList; }
        }

        public IEnumerable<Person> Search(string query)
        {
            //Can alternatively use List<Person> and add a .ToList() in place of IEnumerable, but this will create a deep copy.
            return personList.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

    }
}
