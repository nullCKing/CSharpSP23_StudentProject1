using Microsoft.Maui.Controls;
using Library.StudentManagement.Models;
using System;

namespace Maui.StudentManagement.Dialogs
{
    public partial class AssignmentDialog : ContentPage
    {
        public Course Course { get; set; }

        public AssignmentDialog(Course course, string title)
        {
            InitializeComponent();
            Course = course;
            BindingContext = this;
        }

        private async void ModifyAssignment(object sender, EventArgs e)
        {

        }

        private async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}