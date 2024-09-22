using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeManagement.Model
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}

/*Note: To make AppDbContext class behave like a DBContext class we derive it from EntityFramework core.*/
/*For DBContext class to do any usefull work it needs an instance of DBContext options class this instance carries the
 configuration information such as connection string, database name and so on.*/
/*Note: We will use the DBset property to query and save the instance of Employee class. The link queries we write against 
the DBset of employee property will be translated to SQL quieries against the underlying DB.*/
