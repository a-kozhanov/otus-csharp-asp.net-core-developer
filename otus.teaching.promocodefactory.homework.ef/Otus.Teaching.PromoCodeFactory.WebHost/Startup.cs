using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Otus.Teaching.PromoCodeFactory.DataAccess;
using Otus.Teaching.PromoCodeFactory.WebHost.Extensions;

namespace Otus.Teaching.PromoCodeFactory.WebHost
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // services.AddScoped(typeof(IRepository<Employee>), typeof(EfRepository<Employee>));
            // services.AddScoped(typeof(IRepository<Role>), typeof(EfRepository<Role>));
            // services.AddScoped(typeof(IRepository<Customer>), typeof(EfRepository<Customer>));
            // services.AddScoped(typeof(IRepository<Preference>), typeof(EfRepository<Preference>));
            // services.AddScoped(typeof(IRepository<PromoCode>), typeof(EfRepository<PromoCode>));

            services.AddAutoMapper(typeof(Startup));

            services.RegisterRepositories();

            var connection = Configuration.GetConnectionString("Default");
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("Default"));
            });

            services.AddOpenApiDocument(options =>
            {
                options.Title = "PromoCode Factory API Doc";
                options.Version = "1.0";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            DbInitializer.Initialize(dataContext);

            app.UseOpenApi();
            app.UseSwaggerUi3(x => { x.DocExpansion = "list"; });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}