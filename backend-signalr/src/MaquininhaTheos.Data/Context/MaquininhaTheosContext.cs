using MaquininhaTheos.Data.Mappings;
using MaquininhaTheos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MaquininhaTheos.Data.Context
{
    public class MaquininhaTheosContext : DbContext
    {
        public MaquininhaTheosContext(DbContextOptions<MaquininhaTheosContext> option) 
            : base(option)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<QrCode> QrCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new QrCodeMapping());
        }
    }
}
