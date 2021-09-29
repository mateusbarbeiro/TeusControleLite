using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeusControleLite.Domain.Models;

namespace TeusControleLite.Infrastructure.Context
{
    internal class ProductsMapping : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("products");

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.Active)
                .HasColumnName("active")
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("'1'");

            builder.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

            builder.Property(e => e.Deleted)
                .HasColumnName("deleted")
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.LastChange)
                .HasColumnName("last_change")
                .HasColumnType("datetime");

            builder.Property(e => e.Gtin)
                .HasColumnName("gtin")
                .HasColumnType("varchar(20)");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasColumnType("varchar(200)");

            builder.Property(e => e.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.BrandName)
                .HasColumnName("brand_name")
                .HasColumnType("varchar(200)");

            builder.Property(e => e.GpcCode)
                .HasColumnName("gpc_code")
                .HasColumnType("varchar(100)");

            builder.Property(e => e.GpcDescription)
                .HasColumnName("gpc_description")
                .HasColumnType("varchar(500)");

            builder.Property(e => e.NcmCode)
                .HasColumnName("ncm_code")
                .HasColumnType("varchar(100)");

            builder.Property(e => e.NcmDescription)
                .HasColumnName("ncm_description")
                .HasColumnType("varchar(500)");

            builder.Property(e => e.NcmFullDescription)
                .HasColumnName("ncm_full_description")
                .HasColumnType("varchar(1000)");

            builder.Property(e => e.Thumbnail)
                .HasColumnName("thumbnail")
                .HasColumnType("varchar(250)");

            builder.Property(e => e.InStock)
                .HasColumnName("in_stock")
                .HasColumnType("int")
                .HasDefaultValue(0);
        }
    }
}
