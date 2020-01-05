using Microsoft.EntityFrameworkCore;

namespace MVCDemo.Models.EFCoreDb
{
    public class MovieRateContext : DbContext
    {
        public MovieRateContext(DbContextOptions<MovieRateContext> options)
            : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MemberShipType> MemberShipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
