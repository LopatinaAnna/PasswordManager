using System.ComponentModel;

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
