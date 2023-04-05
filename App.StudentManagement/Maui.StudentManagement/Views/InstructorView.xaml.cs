using Maui.StudentManagement.ViewModels;
using System.Windows.Input;
namespace Maui.StudentManagement.Views
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

    public partial class InstructorView : ContentPage
    {
        private List<ButtonModel> buttonModels;

        public InstructorView()
        {
            InitializeComponent();

            buttonModels = new List<ButtonModel>
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

            ButtonListView.ItemsSource = buttonModels;
        }
        private void CreateCourse_Clicked()
        {
            // Your implementation here
        }

        private void ListAllCourses_Clicked()
        {
            // Your implementation here
        }

        private void SearchCourse_Clicked()
        {
            // Your implementation here
        }

        private void UpdateCourse_Clicked()
        {
            // Your implementation here
        }

        private void AddPerson_Clicked()
        {
            Shell.Current.GoToAsync("//StudentModifier");
        }

        private void RemovePerson_Clicked()
        {
            // Your implementation here
        }

        private void AddAssignment_Clicked()
        {
            // Your implementation here
        }

        private void UpdateAssignment_Clicked()
        {
            // Your implementation here
        }

        private void RemoveAssignment_Clicked()
        {
            // Your implementation here
        }

        private void AddModule_Clicked()
        {
            // Your implementation here
        }

        private void UpdateModule_Clicked()
        {
            // Your implementation here
        }

        private void RemoveModule_Clicked()
        {
            // Your implementation here
        }

        private void AddAnnouncement_Clicked()
        {
            // Your implementation here
        }

        private void UpdateAnnouncement_Clicked()
        {
            // Your implementation here
        }

        private void RemoveAnnouncement_Clicked()
        {
            // Your implementation here
        }

        private void AddAssignmentGroup_Clicked()
        {
            // Your implementation here
        }

        private void RemoveAssignmentGroup_Clicked()
        {
            // Your implementation here
        }

        private void AddGrade_Clicked()
        {
            // Your implementation here
        }

        private void CalculateWeightedGrades_Clicked()
        {
            // Your implementation here
        }
    }
}