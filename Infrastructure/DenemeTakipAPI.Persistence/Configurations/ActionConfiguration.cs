using DenemeTakipAPI.Domain.Entities.ToDo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.Persistence.Configurations
{
    public class ActionConfiguration : IEntityTypeConfiguration<ToDoElement>
    {
        public void Configure(EntityTypeBuilder<ToDoElement> builder)
        {
            builder.HasOne(u => u.User)
                .WithMany(u => u.ToDoElements)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
