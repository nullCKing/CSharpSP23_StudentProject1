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

        public ICommand CreateCourseCommand => new Command(CreateCourse);

        public event PropertyChangedEventHandler PropertyChanged;

        public InstructorViewViewModel()
        {
            ButtonModels = new ObservableCollection<ButtonModel>
            {
                new ButtonModel("Create Course", (ICommand)new Command(CreateCourse_Clicked)),
                new ButtonModel("List All Courses", (ICommand)new Command(ListAllCourses_Clicked)),
                new ButtonModel("Search Course", (ICommand)new Command(SearchCourse_Clicked)),
                new ButtonModel("Update Course", (ICommand)new Command(UpdateCourse_Clicked)),
                new ButtonModel("Add Student", (ICommand)new Command(AddPerson_Clicked)),
                new ButtonModel("Remove Person", (ICommand)new Command(RemovePerson_Clicked)),
                new ButtonModel("Add Assignment", (ICommand)new Command(AddAssignment_Clicked)),
                new ButtonModel("Update Assignment", (ICommand)new Command(UpdateAssignment_Clicked)),
                new ButtonModel("Remove Assignment", (ICommand)new Command(RemoveAssignment_Clicked)),
                new ButtonModel("Add Module", (ICommand)new Command(AddModule_Clicked)),
                new ButtonModel("Update Module", (ICommand)new Command(UpdateModule_Clicked)),
                new ButtonModel("Remove Module", (ICommand)new Command(RemoveModule_Clicked)),
                new ButtonModel("Add Announcement", (ICommand)new Command(AddAnnouncement_Clicked)),
                new ButtonModel("Update Announcement", (ICommand)new Command(UpdateAnnouncement_Clicked)),
                new ButtonModel("Remove Announcement", (ICommand)new Command(RemoveAnnouncement_Clicked)),
                new ButtonModel("Add Assignment Group", (ICommand)new Command(AddAssignmentGroup_Clicked)),
                new ButtonModel("Remove Assignment Group", (ICommand)new Command(RemoveAssignmentGroup_Clicked)),
                new ButtonModel("Add Grade", (ICommand)new Command(AddGrade_Clicked)),
                new ButtonModel("Calculate Weighted Grades", (ICommand)new Command(CalculateWeightedGrades_Clicked))

            };

        }

        private void CreateCourse()
        {
           
        }

        private void CreateCourse_Clicked()
        {

        }

        private void ListAllCourses_Clicked()
        {
           
        }

        private void SearchCourse_Clicked()
        {
           
        }

        private void UpdateCourse_Clicked()
        {
            
        }

        private void AddPerson_Clicked()
        {
            Shell.Current.GoToAsync("//StudentModifier");
        }

        private void RemovePerson_Clicked()
        {
            
        }

        private void AddAssignment_Clicked()
        {
        
        }

        private void UpdateAssignment_Clicked()
        {
          
        }

        private void RemoveAssignment_Clicked()
        {
           
        }

        private void AddModule_Clicked()
        {
           
        }

        private void UpdateModule_Clicked()
        {
            
        }

        private void RemoveModule_Clicked()
        {
            
        }

        private void AddAnnouncement_Clicked()
        {
            
        }

        private void UpdateAnnouncement_Clicked()
        {
           
        }

        private void RemoveAnnouncement_Clicked()
        {
           
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

