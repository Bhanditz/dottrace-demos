using System.Data.Entity;

namespace NerdDinner.Models
{
    public class NerdDinners : DbContext
    {
        static NerdDinners() {
            Database.SetInitializer<NerdDinners>(null);
        }

        public NerdDinners()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }
    }
}