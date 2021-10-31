using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.DataAccess.Data;
using Otus.Teaching.Pcf.Administration.DataAccess.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Administration.WebHost.MongoConfiguration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Otus.Teaching.Pcf.Administration.WebHost
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddMvcOptions(x =>
                x.SuppressAsyncSuffixInActionNames = false);

            // services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            // services.AddScoped<IDbInitializer, EfDbInitializer>();

            services.ConfigureMongoDb(Configuration);

            services.AddScoped<IDbInitializer, MongoInitializer>();
            services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

            // services.AddDbContext<DataContext>(x =>
            // {
            //     //x.UseSqlite("Filename=PromocodeFactoryAdministrationDb.sqlite");
            //     x.UseNpgsql(Configuration.GetConnectionString("PromocodeFactoryAdministrationDb"));
            //     x.UseSnakeCaseNamingConvention();
            //     x.UseLazyLoadingProxies();
            // });

            services.AddOpenApiDocument(options =>
            {
                options.Title = "PromoCode Factory Administration API Doc";
                options.Version = "1.0";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3(x => { x.DocExpansion = "list"; });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            dbInitializer.InitializeDb();
        }
    }

    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureMongoDb(this IServiceCollection services,
            IConfiguration configuration)
        {
            var mongoConfigurations =
                configuration.GetSection(MongoDbConfiguration.SectionName).Get<MongoDbConfiguration>();

            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

            BsonClassMap.RegisterClassMap<Employee>();

            BsonClassMap.RegisterClassMap<Role>();

            services
                .AddSingleton<IMongoClient>(_ =>
                    new MongoClient(configuration.GetConnectionString("MongoDb")))
                .AddSingleton(serviceProvider =>
                    serviceProvider.GetRequiredService<IMongoClient>()
                        .GetDatabase(mongoConfigurations.DatabaseName))
                .AddSingleton(serviceProvider =>
                    serviceProvider.GetRequiredService<IMongoDatabase>()
                        .GetCollection<Employee>(mongoConfigurations.EmployeesCollectionName))
                .AddSingleton(serviceProvider =>
                    serviceProvider.GetRequiredService<IMongoDatabase>()
                        .GetCollection<Role>(mongoConfigurations.RolesCollectionName))
                .AddScoped(serviceProvider =>
                    serviceProvider.GetRequiredService<IMongoClient>()
                        .StartSession());
            return services;
        }
    }
}