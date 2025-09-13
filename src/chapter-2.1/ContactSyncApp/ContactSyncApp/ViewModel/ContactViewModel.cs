using CommunityToolkit.Mvvm.ComponentModel;

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
    }
}
