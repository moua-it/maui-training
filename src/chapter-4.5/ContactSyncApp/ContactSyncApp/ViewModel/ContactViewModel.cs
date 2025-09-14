using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactSyncApp.Dal;

namespace ContactSyncApp.ViewModel
{
    public partial class ContactViewModel(ContactRepository repository) : ObservableObject
    {
        [ObservableProperty]
        private int contactId;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private DateTime birthday;

        [ObservableProperty]
        private string address;

        [ObservableProperty]
        private string city;

        [ObservableProperty]
        private string state;

        [ObservableProperty]
        private string phoneNumber;

        [ObservableProperty]
        private string email;

        [RelayCommand]
        private async Task SaveContact()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Warning", "The Name field is required.", "OK");
                return;
            }

            var contact = new Model.Contact
            {
                ContactId = contactId,
                Name = name,
                Birthday = birthday,
                Address = address,
                City = city,
                State = state,
                PhoneNumber = phoneNumber,
                Email = email
            };

            try
            {
                if (contact.ContactId == 0)
                    await repository.AddAsync(contact);
                else
                    await repository.UpdateAsync(contact);

                await Shell.Current.DisplayAlert("Success", "Contact saved successfully.", "OK");

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while saving the contact.\n{ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteContact()
        {
            bool confirm = await Shell.Current.DisplayAlert(
            "Confirmation",
            "Are you sure you want to delete this contact?",
            "Yes", "No");

            if (!confirm)
                return;

            try
            {
                await repository.DeleteAsync(contactId);
                await Shell.Current.DisplayAlert("Success", "Contact deleted successfully.", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while deleting the contact.\n{ex.Message}", "OK");
            }
        }

    }
}
