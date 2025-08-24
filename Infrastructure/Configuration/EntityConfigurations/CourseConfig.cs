using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntityConfigurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.Image)
                .IsRequired();

            builder.Property(c => c.Instructor)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Type)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(c => c.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(c => c.Modality)
                .HasConversion<string>();

            builder.Property(c => c.Date);

            builder.Property(c => c.Link)
                .HasMaxLength(300);

            builder.OwnsOne(c => c.Price, price =>
            {
                price.Property(p => p.Amount).HasColumnName("PriceAmount").IsRequired();
                price.Property(p => p.Currency).HasColumnName("PriceCurrency").IsRequired();
            });

        }
    }

}
