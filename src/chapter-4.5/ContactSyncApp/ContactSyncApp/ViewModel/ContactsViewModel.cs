using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactSyncApp.Dal;
using ContactSyncApp.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ContactSyncApp.ViewModel;

public partial class ContactsViewModel : ObservableObject
{
    private readonly ContactRepository _repository;
    private bool isBusy = false;

    public ICommand ContactSelected { get; }

    [ObservableProperty]
    private ObservableCollection<Model.Contact> contacts;

    public ContactsViewModel(ContactRepository repository)
    {
        _repository = repository;
        ContactSelected = new Command<int>(OnContactSelected);
    }

    public async Task LoadContactsAsync()
    {
        try
        {
            var contacts = await _repository.GetAllAsync();

            Contacts = new ObservableCollection<Model.Contact>(
                contacts.OrderBy(o => o.Name)
            );
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred while loading the contacts.\n{ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async void OnContactSelected(int contactId)
    {
        if (isBusy) return;

        try
        {
            isBusy = true;

            var contact = await _repository.GetByIdAsync(contactId);

            await Shell.Current.GoToAsync(nameof(ContactFormPage), true, new Dictionary<string, object>
            {
                { "contact", contact }
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred while loading the contact.\n{ex.Message}", "OK");
        }
        finally
        {
            isBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddNewContact()
    {
        if (isBusy) return;

        try
        {
            isBusy = true;

            await Shell.Current.GoToAsync(nameof(ContactFormPage));
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred while trying to add a new contact.\n{ex.Message}", "OK");
        }
        finally
        {
            isBusy = false;
        }
    }
}
