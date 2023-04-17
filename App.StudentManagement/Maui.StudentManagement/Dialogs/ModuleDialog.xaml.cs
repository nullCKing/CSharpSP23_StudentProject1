using Microsoft.Maui.Controls;
using Library.StudentManagement.Models;
using System;

namespace Maui.StudentManagement.Dialogs
{
    public partial class ModuleDialog : ContentPage
    {
        public Course Course { get; set; }

        public ModuleDialog(Course course, string title)
        {
            InitializeComponent();
            Course = course;
            BindingContext = course;
        }

        private async void ModifyModule(object sender, EventArgs e)
        {
        }

        private async void BackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}