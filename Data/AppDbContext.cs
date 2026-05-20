using Microsoft.EntityFrameworkCore;
using YourPetHealth.Models;

namespace YourPetHealth.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Responsavel> Responsaveis { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<HistoricoClinico> HistoricosClinicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Responsavel>()
                .Property(r => r.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<Veterinario>()
                .Property(v => v.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<Pet>()
                .Property(p => p.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<Consulta>()
                .Property(c => c.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<HistoricoClinico>()
                .Property(h => h.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Responsavel)
                .WithMany(r => r.Pets)
                .HasForeignKey(p => p.ResponsavelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Pet)
                .WithMany(p => p.Consultas)
                .HasForeignKey(c => c.PetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Veterinario)
                .WithMany(v => v.Consultas)
                .HasForeignKey(c => c.VeterinarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistoricoClinico>()
                .HasOne(h => h.Pet)
                .WithMany(p => p.Historicos)
                .HasForeignKey(h => h.PetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}