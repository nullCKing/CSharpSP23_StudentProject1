using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.StudentManagement.Models;

namespace Maui.StudentManagement.ViewModels
{
    public class CourseDetailViewModel
    {
        public CourseDetailViewModel(Course course = null)
        {
            if (course != null)
            {
                Course = course;
            }
            else
            {
                Course = new Course();
            }
        }
        
        public Course Course { get; set; }
        public List<Assignment> Assignments => Course.Assignments;
        public List<Announcement> Announcements => Course.Announcements;
        public List<Module> Modules => Course.Modules;
    }

    public class SelectableAssignment
    {
        public Assignment Assignment { get; set; }
        public bool IsSelected { get; set; }

        public SelectableAssignment(Assignment assignment)
        {
            Assignment = assignment;
        }
    }
}