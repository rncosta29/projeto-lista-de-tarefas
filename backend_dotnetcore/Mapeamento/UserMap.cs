using backend_dotnetcore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_dotnetcore.Mapeamento
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Cpf).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.ConfirmPassword).IsRequired();

            builder.HasMany(u => u.TaskModel).WithOne(u => u.User);

            builder.ToTable("Usuario");
        }
    }
}
