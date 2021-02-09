using MaquininhaTheos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaquininhaTheos.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
             .HasColumnType("nvarchar(max)");

            builder.Property(x => x.Chave)
             .HasColumnType("int");

            builder.Property(x => x.DataConexao)
            .HasColumnType("datetime");
        }
    }
}
