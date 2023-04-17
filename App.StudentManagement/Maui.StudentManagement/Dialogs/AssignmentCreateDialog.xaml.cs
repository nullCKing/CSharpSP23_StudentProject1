using Microsoft.Maui.Controls;
using Library.StudentManagement.Models;

namespace Maui.StudentManagement.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssignmentCreateDialog : ContentPage
    {
        public Course Course { get; set; }

        public AssignmentCreateDialog(Course course, string title)
        {
            InitializeComponent();
            Course = course;
            DialogTitle.Text = title;
        }



        private async void OnCancelClicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnCreateClicked(object sender, System.EventArgs e)
        {
            string assignmentName = AssignmentNameEntry.Text;
            string assignmentDescription = AssignmentDescriptionEntry.Text;

            if (!string.IsNullOrEmpty(assignmentName))
            {
                var newAssignment = new Assignment { Name = assignmentName, Description = assignmentDescription };
                Course.Assignments.Add(newAssignment);
            }

            await Navigation.PopModalAsync();
        }
    }
}