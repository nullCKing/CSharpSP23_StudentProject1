using Library.StudentManagement.Models;
using Library.StudentManagement.Services;
using Maui.StudentManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Maui.StudentManagement.ViewModels
{
    public class CourseListViewViewModel : INotifyPropertyChanged
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
                    FilterCourse();
                }
            }
        }

        public ObservableCollection<Course> Course
        {
            get
            {
                return new ObservableCollection<Course>(CourseService.Current.Courses);
            }
        }

        private ObservableCollection<Course> _filteredCourse;
        public ObservableCollection<Course> FilteredCourse
        {
            get => _filteredCourse;
            set
            {
                _filteredCourse = value;
                NotifyPropertyChanged(nameof(FilteredCourse));
            }
        }

        public ICommand ModifyCourseCommand { get; }

        public CourseListViewViewModel()
        {
            FilteredCourse = Course;
            ModifyCourseCommand = new Command<Course>(ModifyCourse);
        }

        private async void FilterCourse()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredCourse = Course;
            }
            else
            {
                var searchTextLower = SearchText.ToLower();
                FilteredCourse = new ObservableCollection<Course>(Course.Where(p => p.Name.ToLower().Contains(searchTextLower)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Course));
            FilterCourse();
        }

        private async void ModifyCourse(Course course)
        {
            var courseDetailView = new CourseDetailView(course);
            courseDetailView.CourseUpdated += (sender, e) => RefreshView();
            await Shell.Current.Navigation.PushAsync(courseDetailView);
        }
    }
}