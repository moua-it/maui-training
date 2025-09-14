using ContactSyncApp;
using ContactSyncApp.View;

namespace ContactSyncApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("ContactFormPage", typeof(ContactFormPage));
        }
    }
}
