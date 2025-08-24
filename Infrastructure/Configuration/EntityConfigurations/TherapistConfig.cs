﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration.EntityConfigurations
{
    public class TherapistConfig : IEntityTypeConfiguration<Therapist>
    {
        public void Configure(EntityTypeBuilder<Therapist> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Bio)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(t => t.Image)
                .IsRequired();

            builder.Property(t => t.Experience)
                .IsRequired();

            builder.Property(t => t.Education)
                .IsRequired();

            builder.HasMany(t => t.Specialties)
                .WithMany()
                .UsingEntity(j => j.ToTable("SpecialtyTherapist"));

        }
    }

}
