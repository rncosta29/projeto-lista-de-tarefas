using backend_dotnetcore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_dotnetcore.Mapeamento
{
    public class TaskMap : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            // builder.Property(t => t.MacAddress).IsRequired();
            builder.Property(t => t.Type).IsRequired();
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Description).IsRequired();
            builder.Property(t => t.When).IsRequired();

            builder.HasOne(t => t.User).WithMany(t => t.TaskModel).HasForeignKey(t => t.UserId);

            builder.ToTable("Listas");
        }
    }
}
