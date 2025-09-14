using ContactSyncApp.ViewModel;

namespace ContactSyncApp.View;

[QueryProperty(nameof(SelectedContact), "contact")]
public partial class ContactFormPage : ContentPage
{
    private readonly ContactViewModel _viewModel;

    public Model.Contact SelectedContact { get; set; }

    public ContactFormPage(ContactViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.LoadViewModelData(SelectedContact);
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}
