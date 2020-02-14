using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using WPF_SQLITE_PasswordManager.Models;
using WPF_SQLITE_PasswordManager.Views;

namespace WPF_SQLITE_PasswordManager.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        ApplicationContext db;

        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand removeCommand;
        RelayCommand copyCommand;

        IEnumerable<PasswordModel> passwordModels;

        public IEnumerable<PasswordModel> PasswordModels
        {
            get { return passwordModels; }
            set
            {
                passwordModels = value;
                OnPropertyChanged();
            }
        }

        public ViewModel()
        {
            TryConnect();

            db = new ApplicationContext();
            db.PasswordModels.Load();
            PasswordModels = db.PasswordModels.Local.ToBindingList();
        }

        public void TryConnect()
        {
            if (!File.Exists(@".\pmanager.db")) 
            {
                SQLiteConnection.CreateFile(@".\pmanager.db");

                using (SQLiteConnection connection = new SQLiteConnection("Data Source=pmanager.db;"))
                {
                    string commandCreate = "CREATE TABLE  IF NOT EXISTS PasswordModels ( Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Title TEXT, Login TEXT, Password TEXT) ";
                    SQLiteCommand CommandCreate = new SQLiteCommand(commandCreate, connection);

                    string commandInsert = "INSERT INTO PasswordModels ([Title], [Login], [Password] ) VALUES ( @Title, @Login, @Password)";
                    SQLiteCommand CommandInsert = new SQLiteCommand(commandInsert, connection);
                    var p = new PasswordModel() { Id = 1, Login = "mylogin", Password = "123456", Title = "Test title" };
                    CommandInsert.Parameters.AddWithValue("@Title", p.Title);
                    CommandInsert.Parameters.AddWithValue("@Login", p.Login);
                    CommandInsert.Parameters.AddWithValue("@Password", p.Password);
                    
                    connection.Open(); 
                    CommandCreate.ExecuteNonQuery();
                    CommandInsert.ExecuteNonQuery();
                    connection.ChangePassword("apppassword");
                    connection.Close();
                }
            }
        }

        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      EditWindow editWindow = new EditWindow(new PasswordModel());
                      if (editWindow.ShowDialog() == true)
                      {
                          PasswordModel pm = editWindow.PasswordModel;
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
                      EditWindow editWindow = new EditWindow(pm);


                      if (editWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          selected = db.PasswordModels.Find(editWindow.PasswordModel.Id);
                          if (selected != null)
                          {
                              selected.Login = editWindow.PasswordModel.Login;
                              selected.Title = editWindow.PasswordModel.Title;
                              selected.Password = editWindow.PasswordModel.Password;
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

        // команда копирования пароля в буфер
        public RelayCommand CopyCommand
        {
            get
            {
                return copyCommand ??
                  (copyCommand = new RelayCommand((obj) =>
                  {
                      if (obj == null) return;
                      Clipboard.Clear();
                      Clipboard.SetText(obj as string);
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
