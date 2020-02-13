using System.Windows;
using WPF_SQLITE_PasswordManager.Models;

namespace WPF_SQLITE_PasswordManager.Views
{
    public partial class EditWindow : Window
    {
        public PasswordModel PasswordModel;
        public EditWindow(PasswordModel passwordModel)
        {
            InitializeComponent();
            PasswordModel = passwordModel;
            this.DataContext = PasswordModel;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
