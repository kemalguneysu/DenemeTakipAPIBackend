using DenemeTakipAPI.Domain.Entities.DenemeFolder.TytFolder;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenemeTakipAPI.Domain.Entities.DenemeFolder.AytFolder;

namespace DenemeTakipAPI.Persistence.Configurations
{
    public class AytConfiguration : IEntityTypeConfiguration<AytDeneme>
    {
        public void Configure(EntityTypeBuilder<AytDeneme> builder)
        {
            builder
            .HasOne(td => td.User)
            .WithMany(u => u.AytDenemes)
            .HasForeignKey(td => td.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
