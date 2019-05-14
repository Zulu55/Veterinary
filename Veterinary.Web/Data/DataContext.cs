namespace Veterinary.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Veterinary.Web.Data.Entities;

    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Owner> Owners { get; set; }

        public DbSet<PetType> PetTypes { get; set; }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<History> Histories { get; set; }

        public DbSet<Agenda> Agendas { get; set; }
    }
}
