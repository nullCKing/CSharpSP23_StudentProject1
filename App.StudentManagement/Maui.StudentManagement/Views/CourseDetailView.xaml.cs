using Maui.StudentManagement.ViewModels;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;
using Maui.StudentManagement.Dialogs;

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

    private async void AddModuleClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CourseDetailViewModel;
        var course = viewModel.Course;
        var moduleCreateDialog = new ModuleCreateDialog(course, "Add Module");
        await Navigation.PushModalAsync(moduleCreateDialog);
    }

    private async void ViewModuleClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CourseDetailViewModel; 
        var course = viewModel.Course;
        var moduleDialog = new ModuleDialog(course, "View Modules");
        await Navigation.PushModalAsync(moduleDialog);
    }

    private async void AddAssignmentClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CourseDetailViewModel;
        var course = viewModel.Course;
        var assignmentCreateDialog = new AssignmentCreateDialog(course, "Add Assignment");
        await Navigation.PushModalAsync(assignmentCreateDialog);
    }

    private async void ViewAssignmentClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CourseDetailViewModel;
        var course = viewModel.Course;
        var assignmentDialog = new AssignmentDialog(course, "View Assignments");
        await Navigation.PushModalAsync(assignmentDialog);
    }

    private async void AddAnnouncementClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CourseDetailViewModel;
        var course = viewModel.Course;
        var announcementCreateDialog = new AnnouncementCreateDialog(course, "Add Announcement");
        await Navigation.PushModalAsync(announcementCreateDialog);
    }

    private async void ViewAnnouncementClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CourseDetailViewModel;
        var course = viewModel.Course;
        var announcementDialog = new AnnouncementDialog(course, "View Announcements");
        await Navigation.PushModalAsync(announcementDialog);
    }

}