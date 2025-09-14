using ContactSyncApp.ViewModel;

namespace ContactSyncApp.View;

public partial class ContactListPage : ContentPage
{
    private readonly ContactsViewModel _viewModel;

    public ContactListPage(ContactsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Task.Run(async () => await _viewModel.LoadContactsAsync()).GetAwaiter().GetResult();
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}