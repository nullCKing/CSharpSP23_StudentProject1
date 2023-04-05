using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Maui.StudentManagement.ViewModels;
using System;

namespace Maui.StudentManagement
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void StudentButton_Click(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Student");
        }

        private void InstructorButton_Click(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Instructor");
        }

        private void TAButton_Click(object sender, EventArgs e)
        {
            // Handle the TA button click
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            // Handle the Exit button click
        }
    }
}