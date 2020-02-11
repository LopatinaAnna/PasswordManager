using System.Windows;

namespace WPF_SQLITE_PasswordManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel();
        }
    }
}
