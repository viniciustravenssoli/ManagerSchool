
using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infra.Mappings
{
    public class ClassMap : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Classes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType("BIGINT");

            builder.Property(x => x.ClassCode)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("classCode");

            builder.Property(x => x.Discipline)
                .IsRequired()
                .HasMaxLength(120)
                .HasColumnName("discipline");

            builder.Property(x => x.Price)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("price");

        }
    }
}