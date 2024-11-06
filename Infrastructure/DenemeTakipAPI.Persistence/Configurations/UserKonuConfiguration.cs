using DenemeTakipAPI.Domain.Entities.DenemeFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Configurations
{
    public class UserKonuConfiguration : IEntityTypeConfiguration<UserKonu>
    {
        public void Configure(EntityTypeBuilder<UserKonu> builder)
        {
            builder.HasOne(u => u.Konu)
                 .WithMany(u => u.UserKonular)
                 .HasForeignKey(u => u.KonuId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.User)
                 .WithMany(u => u.UserKonular)
                 .HasForeignKey(u => u.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(u => new { u.UserId, u.KonuId}).IsUnique();
        }
    }
}
