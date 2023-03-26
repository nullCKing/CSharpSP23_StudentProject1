using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using App.StudentManagement.Helpers;

namespace WPF.StudentManagement
{
    public partial class StudentLoginPage : Page
    {
        public StudentLoginPage()
        {
            InitializeComponent();
        }

        private void StudentContinueButton_Click(object sender, RoutedEventArgs e)
        {
            StudentLoginFrame.Content = new StudentNavPage();
            LoginPageContent.Visibility = Visibility.Collapsed;
            StudentLoginFrame.Visibility = Visibility.Visible;
        }
    }
}
