using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Task_20.Model.Data;

namespace Task_20.Model
{
    public class DbPhoneBook : DbContext
    {
        public DbPhoneBook(string options) : base(options)
        {
 

        }

        public DbSet<Person> DatabasePerson { get; set; }

        public DbSet<PersonalData> DatabasePersonalData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
