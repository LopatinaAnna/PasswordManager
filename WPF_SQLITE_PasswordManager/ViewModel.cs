using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;

namespace WPF_SQLITE_PasswordManager
{
    public class ViewModel : INotifyPropertyChanged
    {
        ApplicationContext db;

        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand removeCommand;

        IEnumerable<PasswordModel> passwordModels;

        public IEnumerable<PasswordModel> PasswordModels
        {
            get { return passwordModels; }
            set
            {
                passwordModels = value;
                OnPropertyChanged("PasswordModels");
            }
        }

        public ViewModel()
        {
            db = new ApplicationContext();
            db.PasswordModels.Load();
            PasswordModels = db.PasswordModels.Local.ToBindingList();
        }

        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      ChangeWindow changeWindow = new ChangeWindow(new PasswordModel());
                      if (changeWindow.ShowDialog() == true)
                      {
                          PasswordModel pm = changeWindow.PasswordModel;
                          db.PasswordModels.Add(pm);
                          db.SaveChanges();
                      }
                  }));
            }
        }

        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;

                      // получаем выделенный объект
                      PasswordModel selected = selectedItem as PasswordModel;

                      PasswordModel pm = new PasswordModel()
                      {
                          Id = selected.Id,
                          Login = selected.Login,
                          Password = selected.Password,
                          Title = selected.Title
                      };
                      ChangeWindow changeWindow = new ChangeWindow(pm);


                      if (changeWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          selected = db.PasswordModels.Find(changeWindow.PasswordModel.Id);
                          if (selected != null)
                          {
                              selected.Login = changeWindow.PasswordModel.Login;
                              selected.Title = changeWindow.PasswordModel.Title;
                              selected.Password = changeWindow.PasswordModel.Password;
                              db.Entry(selected).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }

        // команда удаления
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      PasswordModel pm = selectedItem as PasswordModel;
                      db.PasswordModels.Remove(pm);
                      db.SaveChanges();
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
