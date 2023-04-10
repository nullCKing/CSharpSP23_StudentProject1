using Library.StudentManagement.Models;
using Library.StudentManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.StudentManagement.ViewModels
{
    public class StudentListViewViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    NotifyPropertyChanged(nameof(SearchText));
                    FilterPeople();
                }
            }
        }

        public ObservableCollection<Person> People
        {
            get
            {
                return new ObservableCollection<Person>(PersonService.Current.Persons);
            }
        }

        private ObservableCollection<Person> _filteredPeople;
        public ObservableCollection<Person> FilteredPeople
        {
            get => _filteredPeople;
            set
            {
                _filteredPeople = value;
                NotifyPropertyChanged(nameof(FilteredPeople));
            }
        }

        public StudentListViewViewModel()
        {
            FilteredPeople = People;
        }

        private void FilterPeople()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredPeople = People;
            }
            else
            {
                var searchTextLower = SearchText.ToLower();
                FilteredPeople = new ObservableCollection<Person>(People.Where(p => p.Name.ToLower().Contains(searchTextLower)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(People));
        }
    }
}