using DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Configurations
{
    public class TytConfiguration : IEntityTypeConfiguration<TytDeneme>
    {
        public void Configure(EntityTypeBuilder<TytDeneme> builder)
        {
            builder
            .HasOne(td => td.User)
            .WithMany(u => u.TytDenemes)
            .HasForeignKey(td => td.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
