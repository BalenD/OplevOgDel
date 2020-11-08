using Microsoft.EntityFrameworkCore;
using OplevOgDel.Api.Data.Models;

namespace OplevOgDel.Api.Data
{
    public class OplevOgDelDbContext : DbContext
    {
        public DbSet<User>  Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ListOfExperiences> ListOfExperiences { get; set; }
        public OplevOgDelDbContext(DbContextOptions<OplevOgDelDbContext> options) : base(options)
        {

        }

        // add seed data here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
