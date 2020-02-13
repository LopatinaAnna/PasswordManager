using System.Windows;
using WPF_SQLITE_PasswordManager.ViewModels;

namespace WPF_SQLITE_PasswordManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
        }
    }
}
