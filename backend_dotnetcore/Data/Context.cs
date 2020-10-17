using backend_dotnetcore.Mapeamento;
using backend_dotnetcore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace backend_dotnetcore.Data
{
    public class Context : IdentityDbContext
    {
        public DbSet<UserModel> Usuario { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }



        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TaskMap());
            builder.ApplyConfiguration(new UserMap());
        }
    }
}
