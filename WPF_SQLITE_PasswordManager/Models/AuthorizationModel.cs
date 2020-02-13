using System.ComponentModel;
using WPF_SQLITE_PasswordManager.Views;

namespace WPF_SQLITE_PasswordManager.Models
{
    public class AuthorizationModel : INotifyPropertyChanged
    {
        private string enterPassword;

        public string EnterPassword
        {
            get { return enterPassword; }
            set
            {
                enterPassword = value;
                OnPropertyChanged();
            }
        }

        RelayCommand checkEnterCommand;
        public RelayCommand CheckEnterCommand
        {
            get
            {
                return checkEnterCommand ??
                  (checkEnterCommand = new RelayCommand((obj) =>
                  {
                          if (obj as string != "1234")
                          {
                              AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                          }
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
