using BooksProject.Models;
using BooksProject.Models.Context;
using BooksProject.Repository;
using BooksProject.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BooksProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // EntityFramework and PostgreSQL configuration
            services.AddDbContext<ApplicationDbContext>(options => 
            {
                options.UseNpgsql(Configuration.GetConnectionString("PostgreSQLConnectionString"));
            });

            // Serialization Cicle/Loop Configure
            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Swagger Configure
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BooksProject", Version = "v1" });
            });

            // Dependency Injection (DI)
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRepository<Book>, BookRepository>();
            services.AddScoped<IRepository<Gender>, GenderRepository>();
            services.AddScoped<IRepository<Editor>, EditorRepository>();
            services.AddScoped<IRepository<Identifier>, IdentifierRepository>();
            services.AddScoped<IRepository<Author>, AuthorRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BooksProject v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
