using Maui.StudentManagement.ViewModels;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;
namespace Maui.StudentManagement.Views;

public partial class CourseDetailView : ContentPage
{
    public CourseDetailView()
    {
        InitializeComponent();

        BindingContext = new CourseDetailViewModel();
    }

    private void OkClick(object sender, EventArgs e)
    {
        var context = BindingContext as CourseDetailViewModel;

        CourseService.Current.Add(new Course { Name = context.Name, Code = context.Code, Description = context.Description });
        Shell.Current.GoToAsync("//MainPage");
    }
}