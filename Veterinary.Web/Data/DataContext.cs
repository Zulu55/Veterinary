namespace Veterinary.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using Veterinary.Web.Data.Entities;

    public class DataContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
