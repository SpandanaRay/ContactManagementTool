using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ContactDatabaseContext : IdentityDbContext<User,Role,int>
    {
        //only for migration
        public ContactDatabaseContext()
        {

        }
        //setting form startup.cs file
        public ContactDatabaseContext(DbContextOptions<ContactDatabaseContext> options) : base(options)
        {

        }
        public DbSet<Contact> Contact { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Migration purpose
                optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; initial catalog=DOTNET_CORE_ASSESMENT;persist security info=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
