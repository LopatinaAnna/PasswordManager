using System.Data.Entity;
using WPF_SQLITE_PasswordManager.Models;

namespace WPF_SQLITE_PasswordManager
{
    class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<PasswordModel> PasswordModels { get; set; }
    }
}
