using System.Windows;

namespace WPF_SQLITE_PasswordManager
{
    public partial class ChangeWindow : Window
    {
        public PasswordModel PasswordModel;
        public ChangeWindow(PasswordModel passwordModel)
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
