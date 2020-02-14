using System.Windows;

namespace WPF_SQLITE_PasswordManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();

            InitializeComponent();
        }
    }
}
