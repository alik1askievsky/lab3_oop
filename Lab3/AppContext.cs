using Lab3.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Participant> Participants => Set<Participant>();
        public DbSet<Award> Awards => Set<Award>();
        public DbSet<Division> Divisions => Set<Division>();
        public DbSet<Module> Modules => Set<Module>();
        public DbSet<ParticipantModule> ParticipantModules => Set<ParticipantModule>();

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=helloapp.db");
            }
        }
    }
}
