using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactSyncApp.Dal;

namespace ContactSyncApp.ViewModel
{
    public partial class ContactViewModel(ContactRepository repository) : ObservableObject
    {
        private bool isBusy = false;

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

        [ObservableProperty]
        private bool hideDeleteButton;

        [RelayCommand]
        private async Task SaveContact()
        {
            if (isBusy) return;

            try
            {
                isBusy = true;

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

                if (contact.ContactId == 0)
                    await repository.AddAsync(contact);
                else
                    await repository.UpdateAsync(contact);

                await Shell.Current.DisplayAlert("Success", "Contact saved successfully.", "OK");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while saving the contact.\n{ex.Message}", "OK");
            }
            finally
            {
                isBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteContact()
        {
            if (isBusy) return;

            try
            {
                isBusy = true;

                bool confirm = await Shell.Current.DisplayAlert(
                "Confirmation",
                "Are you sure you want to delete this contact?",
                "Yes", "No");

                if (!confirm)
                    return;

                await repository.DeleteAsync(contactId);

                await Shell.Current.DisplayAlert("Success", "Contact deleted successfully.", "OK");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred while deleting the contact.\n{ex.Message}", "OK");
            }
            finally
            {
                isBusy = false;
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            if (isBusy) return;

            try
            {
                isBusy = true;

                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                isBusy = false;
            }
        }

        public void LoadViewModelData(Model.Contact contact)
        {
            HideDeleteButton = true;

            if (contact == null) return;

            HideDeleteButton = false;

            ContactId = contact.ContactId;
            Name = contact.Name;
            Birthday = contact.Birthday;
            Address = contact.Address;
            City = contact.City;
            State = contact.State;
            PhoneNumber = contact.PhoneNumber;
            Email = contact.Email;
        }
    }
}
