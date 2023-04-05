using Library.StudentManagement.Models;
using Library.StudentManagement.Services;
using MAUI.LearningManagement.ViewModels;

namespace MAUI.LearningManagement.Views;

public partial class PersonDetailView : ContentPage
{
	public PersonDetailView()
	{
		InitializeComponent();

		BindingContext = new PersonDetailViewModel();
	}

    private void OkClick(object sender, EventArgs e)
    {
		var context = BindingContext as PersonDetailViewModel;
		StudentClassification classification;
		switch (context.ClassificationString)
		{
			case "S":
				classification = StudentClassification.Senior;
				break;
			case "J":
				classification = StudentClassification.Junior;
				break;
			case "O":
				classification = StudentClassification.Sophomore;
				break;
			case "F":
			default:
				classification = StudentClassification.Freshman;
				break;
		}
		PersonService.Current.Add(new Student { Name = context.Name, Classification = classification });
        Shell.Current.GoToAsync("//MainPage");
    }
}