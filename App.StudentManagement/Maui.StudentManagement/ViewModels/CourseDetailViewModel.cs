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
    }
}