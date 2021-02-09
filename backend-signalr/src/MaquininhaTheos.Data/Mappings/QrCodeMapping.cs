using MaquininhaTheos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaquininhaTheos.Data.Mappings
{
    public class QrCodeMapping : IEntityTypeConfiguration<QrCode>
    {
        public void Configure(EntityTypeBuilder<QrCode> builder)
        {
            builder.ToTable("QrCodes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Imagem)
             .HasColumnType("nvarchar(max)");

            builder.Property(x => x.DataCriacao)
            .HasColumnType("datetime");
        }
    }
}
