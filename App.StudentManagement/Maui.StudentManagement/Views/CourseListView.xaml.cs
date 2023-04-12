using System.Collections.ObjectModel;
using System.ComponentModel;
using Library.StudentManagement.Models;
using Library.StudentManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Maui.StudentManagement.ViewModels;

namespace Maui.StudentManagement.Views;

public partial class CourseListView : ContentPage
{
    public CourseListView()
    {
        InitializeComponent();

        BindingContext = new CourseListViewViewModel();
    }

}