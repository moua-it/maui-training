using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ContactSyncApp.ViewModel
{
    public partial class ContactViewModel : ObservableObject
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
        private void SaveContact()
        {
            Console.WriteLine($"Salvando contato: {Name}");
        }

        [RelayCommand]
        private void DeleteContact()
        {
            Console.WriteLine($"Excluindo contato: {Name}");
        }
    }
}
