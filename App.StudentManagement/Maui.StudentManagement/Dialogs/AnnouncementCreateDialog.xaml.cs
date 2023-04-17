using Microsoft.Maui.Controls;
using Library.StudentManagement.Models;

namespace Maui.StudentManagement.Dialogs
{
    public partial class AnnouncementCreateDialog : ContentPage
    {
        public Course Course { get; set; }
        public Announcement Announcement { get; set; }


        public AnnouncementCreateDialog(Course course, string title, Announcement announcement = null)
        {
            InitializeComponent();
            Course = course;
            DialogTitle.Text = title;
            Announcement = announcement;

            if (Announcement != null)
            {
                AnnouncementNameEntry.Text = Announcement.Headline;
                AnnouncementTextEntry.Text = Announcement.InternalText;
                AnnouncementDatePicker.Date = Announcement.Date;
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnCreateClicked(object sender, EventArgs e)
        {
            string announcementName = AnnouncementNameEntry.Text;
            string announcementText = AnnouncementTextEntry.Text;
            DateTime announcementDate = AnnouncementDatePicker.Date;

            if (!string.IsNullOrEmpty(announcementName))
            {
                if (Announcement == null) 
                {
                    var newAnnouncement = new Announcement { Headline = announcementName, InternalText = announcementText, Date = announcementDate };
                    Course.Announcements.Add(newAnnouncement);
                }
                else 
                {
                    Announcement.Headline = announcementName;
                    Announcement.InternalText = announcementText;
                    Announcement.Date = announcementDate;

                }
            }

            await Navigation.PopModalAsync();
        }
    }
}