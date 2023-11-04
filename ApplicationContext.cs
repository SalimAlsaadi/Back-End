using LibraryAPI.Modules;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI
{
    public class ApplicationContext:DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)  //constructor with intial database what configured in appsettins.json file
        {
        
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        //optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=LibraryAPI; Integrated Security=true; TrustServerCertificate=True"); //this database will be not active and i will connect to database from appSetting.Json file

        }

        public DbSet<Book> books { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Borrow> borrowFunctions { get; set; }
        public DbSet<Employee> employees { get; set; }
    }
}
