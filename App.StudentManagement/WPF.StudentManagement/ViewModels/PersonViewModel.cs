using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.StudentManagement.Helpers;
using System.Collections.ObjectModel;
using Library.StudentManagement.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF.StudentManagement.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private PersonHelper personHelper;

        public PersonViewModel()
        {
            personHelper = new PersonHelper();
            LoadPersons();
        }

        private ObservableCollection<Person> persons;
        public ObservableCollection<Person> Persons
        {
            get { return persons; }
            set
            {
                persons = value;
                OnPropertyChanged("Persons");
            }
        }

        private void LoadPersons()
        {
            Persons = new ObservableCollection<Person>(personHelper.Persons);
        }

        // Implement INotifyPropertyChanged and other ViewModel methods for CreatePerson, UpdatePerson, ListStudents, and SearchStudents
    }
}