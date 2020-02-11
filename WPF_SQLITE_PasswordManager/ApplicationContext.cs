using System.Data.Entity;

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
