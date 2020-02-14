using WPF_SQLITE_PasswordManager.Views;
using WPF_SQLITE_PasswordManager.Models;
using System.ComponentModel;

namespace WPF_SQLITE_PasswordManager.ViewModels
{
    class AuthorizationViewModel 
    {
        RelayCommand checkEnterCommand;
        public RelayCommand CheckEnterCommand
        {
            get
            {
                return checkEnterCommand ??
                  (checkEnterCommand = new RelayCommand((obj) =>
                  {
                      AuthorizationModel model = new AuthorizationModel();
                      if (obj as string != "1111")
                      {
                          AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                      }
                  }));
            }
        }
    }
}
