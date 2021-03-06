using System;
using System.IO;
using System.Reflection;
using System.Text;
using backend_dotnetcore.Data;
using backend_dotnetcore.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace backend_dotnetcore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(opcoes => opcoes.UseSqlite(Configuration.GetConnectionString("ConexaoDB")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Lista de Tarefas",
                        Version = "v1",
                        Description = "API focada em realizar tarefas",
                        Contact = new OpenApiContact
                        {
                            Name = "Rafael Costa",
                            Email = "raf.costa29@gmail.com",
                            Url = new Uri("https://github.com/rncosta29")
                        }
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped<IRepository, Repository>();
            services.AddCors();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lista de Tarefas");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
