using ContactSyncApp.ViewModel;

namespace ContactSyncApp.View;

public partial class ContactFormPage : ContentPage
{
	public ContactFormPage(ContactViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}