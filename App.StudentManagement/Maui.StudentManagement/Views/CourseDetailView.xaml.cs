using Maui.StudentManagement.ViewModels;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;
namespace Maui.StudentManagement.Views;

public partial class CourseDetailView : ContentPage
{
    public event EventHandler CourseUpdated;

    public CourseDetailView(Course course = null)
    {
        InitializeComponent();

        BindingContext = new CourseDetailViewModel(course);
    }

    private void OkClick(object sender, EventArgs e)
    {
        var context = BindingContext as CourseDetailViewModel;

        CourseService.Current.AddOrUpdate(context.Course);

        CourseUpdated?.Invoke(this, EventArgs.Empty);

        Shell.Current.GoToAsync("//Instructor");
    }

}