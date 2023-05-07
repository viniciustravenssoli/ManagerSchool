using Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infra.Mappings{
    public class StudentMap : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnType("BIGINT");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(80)");

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnName("cpf")
                .HasColumnType("VARCHAR(11)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(180)
                .HasColumnName("email")
                .HasColumnType("VARCHAR(180)");
            
            builder.Property(x => x.Rgm)
                .IsRequired()
                .HasMaxLength(12)
                .HasColumnName("rgm")
                .HasColumnType("VARCHAR(12)");

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(12)
                .HasColumnName("phone")
                .HasColumnType("VARCHAR(12)");

            builder.Property(x => x.Birth)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("birth")
                .HasColumnType("date");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("createdAt")
                .HasColumnType("date");
        }
    }
}