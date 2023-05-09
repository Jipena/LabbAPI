using LabbAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LabbAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
            
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<PersonInterest> PersonInterests { get; set; }
        public DbSet<WebURL> WebURLs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(

                 new Person
                 {
                     PersonId = 1,
                     FirstName = "Jesper",
                     LastName = "Andersson",
                     PhoneNr = "1231231233"
                 },
                 new Person
                 {
                     PersonId = 2,
                     FirstName = "Jens",
                     LastName = "Jansson",
                     PhoneNr = "2311232322"
                 },
                 new Person
                 {
                     PersonId = 3,
                     FirstName = "Lotta",
                     LastName = "Magnusson",
                     PhoneNr = "3213213211"
                 },
                 new Person
                 {
                     PersonId = 4,
                     FirstName = "Runar",
                     LastName = "Larsson",
                     PhoneNr = "2311321231"
                 },
                 new Person
                 {
                     PersonId = 5,
                     FirstName = "Madde",
                     LastName = "Karlsson",
                     PhoneNr = "3213213213"
                 }
            );
        }
    }
}
