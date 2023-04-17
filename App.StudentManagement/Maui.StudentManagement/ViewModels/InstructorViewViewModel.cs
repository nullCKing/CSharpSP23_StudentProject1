using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Maui.StudentManagement.ViewModels;
using System.Windows.Input;
using Maui.StudentManagement.Views;



namespace Maui.StudentManagement.ViewModels
{
    public class ButtonModel
    {
        public string Text { get; set; }
        public ICommand ClickedCommand { get; set; }

        public ButtonModel(string text, ICommand clickedCommand)
        {
            Text = text;
            ClickedCommand = clickedCommand;
        }
    }

    public class InstructorViewViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Person> People
        {
            get
            {
                return new ObservableCollection<Person>(PersonService.Current.Persons);
            }
        }

        public ObservableCollection<ButtonModel> ButtonModels { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public InstructorViewViewModel()
        {
            ButtonModels = InitializeButtonModels();
        }

        private ObservableCollection<ButtonModel> InitializeButtonModels()
        {
            return new ObservableCollection<ButtonModel>
            {
                CreateButtonModel("Create Course", CreateCourse_Clicked),
                CreateButtonModel("List All Courses", ListAllCourses_Clicked),
                CreateButtonModel("Add Student", AddPerson_Clicked),
                CreateButtonModel("Remove Student", RemovePerson_Clicked),
                CreateButtonModel("List all students", ListAllStudents_Clicked),
                CreateButtonModel("Add Assignment Group", AddAssignmentGroup_Clicked),
                CreateButtonModel("Remove Assignment Group", RemoveAssignmentGroup_Clicked),
                CreateButtonModel("Add Grade", AddGrade_Clicked),
                CreateButtonModel("Calculate Weighted Grades", CalculateWeightedGrades_Clicked)
            };
        }

        private ButtonModel CreateButtonModel(string text, Action action)
        {
            return new ButtonModel(text, new Command(action));
        }

        public async void CreateCourse_Clicked()
        {
            await Shell.Current.Navigation.PushAsync(new CourseDetailView(null));
        }

        private async void ListAllCourses_Clicked()
        {
            await Shell.Current.GoToAsync("//CourseList");
        }

        public async void AddPerson_Clicked()
        {
            await Shell.Current.GoToAsync("//StudentModifier");
        }

        private void RemovePerson_Clicked()
        {
            
        }

        private void ListAllStudents_Clicked()
        {
            Shell.Current.GoToAsync("//StudentList");
        }


        private void AddAssignmentGroup_Clicked()
        {
            
        }

        private void RemoveAssignmentGroup_Clicked()
        {
          
        }

        private void AddGrade_Clicked()
        {
          
        }

        private void CalculateWeightedGrades_Clicked()
        {
            
        }
        
    }
}

