using Microsoft.Maui.Controls;
using Library.StudentManagement.Models;
using Maui.StudentManagement.ViewModels;
using System.Collections.ObjectModel;

namespace Maui.StudentManagement.Dialogs
{
    public partial class ModuleCreateDialog : ContentPage
    {
        public Course Course { get; set; }

        public ObservableCollection<SelectableAssignment> SelectableAssignments { get; set; }

        public ModuleCreateDialog(Course course, string title)
        {
            InitializeComponent();
            Course = course;
            DialogTitle.Text = title;
            SelectableAssignments = new ObservableCollection<SelectableAssignment>(course.Assignments.Select(a => new SelectableAssignment(a)));
            BindingContext = this;
        }

        private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var radioButton = sender as RadioButton;

                AssignmentStackLayout.IsVisible = radioButton == AssignmentRadioButton;
                FileItemStackLayout.IsVisible = radioButton == FileItemRadioButton;
                PageItemStackLayout.IsVisible = radioButton == PageItemRadioButton;
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnCreateClicked(object sender, EventArgs e)
        {
            var moduleName = ModuleNameEntry.Text;
            if (!string.IsNullOrEmpty(moduleName))
            {
                var newModule = new Module { Name = moduleName };

                if (AssignmentRadioButton.IsChecked)
                {
                    foreach (var selectableAssignment in SelectableAssignments)
                    {
                        if (selectableAssignment.IsSelected)
                        {
                            var assignmentItem = new AssignmentItem { Assignment = selectableAssignment.Assignment };
                            newModule.Content.Add(assignmentItem);
                        }
                    }
                }

                Course.Modules.Add(newModule);
            }
            await Navigation.PopModalAsync();
        }
    }
}