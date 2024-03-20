using Microsoft.EntityFrameworkCore;
using Rinha2024.Entitdade;

namespace Rinha2024.Data.Context
{
    public class RinhaContext : DbContext
    {

        public RinhaContext(DbContextOptions<RinhaContext> context) : base(context)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasKey(c => new { c.Id });
            modelBuilder.Entity<Transacao>().HasKey(c => c.Id);
            modelBuilder.Entity<Transacao>().Property(c => c.Tipo).HasConversion<string>();

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, Limite = 100000, Saldo = 0 },
                new Cliente { Id = 2, Limite = 80000, Saldo = 0 },
                new Cliente { Id = 3, Limite = 1000000, Saldo = 0 },
                new Cliente { Id = 4, Limite = 10000000, Saldo = 0 },
                new Cliente { Id = 5, Limite = 500000, Saldo = 0 }
              );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //string db_connection = @"User ID=postgres;
            //                        Password=123456;
            //                        Host=postgres;
            //                        Port=5432;
            //                        Database=Rinha;
            //                        Pooling=true;  
            //                        Maximum Pool Size=200; 
            //                        Connection Lifetime=0;
            //                        Include Error Detail=true";

            string db_connection = Environment.GetEnvironmentVariable("DB_CONNECTION");

            optionsBuilder.UseNpgsql(db_connection);
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Transacao> Transacao { get; set; }
    }
}
