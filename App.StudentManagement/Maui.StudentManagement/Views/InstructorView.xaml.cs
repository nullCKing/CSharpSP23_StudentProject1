using Microsoft.Maui.Controls;
using Maui.StudentManagement.ViewModels;

namespace Maui.StudentManagement.Views
{
    public partial class InstructorView : ContentPage
    {
        public InstructorView()
        {
            InitializeComponent();
            BindingContext = new InstructorViewViewModel();
        }
    }
}