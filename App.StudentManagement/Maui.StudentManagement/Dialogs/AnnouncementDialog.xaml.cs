using Microsoft.Maui.Controls;
using Library.StudentManagement.Models;
using Maui.StudentManagement.ViewModels;
using Maui.StudentManagement.Views;
using Library.StudentManagement.Services;
using System;

namespace Maui.StudentManagement.Dialogs;

public partial class AnnouncementDialog : ContentPage
{
    public Course Course { get; set; }

    public AnnouncementDialog(Course course, string title)
    {
        InitializeComponent();
        Course = course;
        BindingContext = this;
    }

    private async void ModifyAnnouncements(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedAnnouncement = button.CommandParameter as Announcement;
        var course = Course; 

        // Pass the selectedAnnouncement to the AnnouncementCreateDialog
        var announcementCreateDialog = new AnnouncementCreateDialog(course, "Modify Announcement", selectedAnnouncement);
        await Navigation.PushModalAsync(announcementCreateDialog);
    }

    private async void BackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}