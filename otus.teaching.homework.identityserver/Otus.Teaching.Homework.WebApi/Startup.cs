using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddAuthorization(c =>
            {
                c.AddPolicy("ScopeWebApi", p => p.RequireClaim("scope", "ScopeWebApi"));
                c.AddPolicy("ScopeConsoleApp", p => p.RequireClaim("scope", "ScopeConsoleApp"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Otus.Teaching.Homework.IdentityServer", Version = "v1" });


                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:6001/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:6001/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"Api", "Web Api"}
                            }
                        }
                    }
                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { "Api" }
                    }
                });


                // c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                // {
                //     Description = "JWT Authorization header using the Bearer scheme.",
                //     Name = "Authorization",
                //     In = ParameterLocation.Header,
                //     Scheme = "bearer",
                //     Type = SecuritySchemeType.Http,
                //     BearerFormat = "JWT",
                // });
                //
                // c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                // {
                //      new OpenApiSecurityScheme
                //      {
                //        Reference = new OpenApiReference
                //        {
                //          Type = ReferenceType.SecurityScheme,
                //          Id = "Bearer"
                //        }
                //       },
                //       new string[] { }
                //     }
                // });

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            },
                //            Scheme = "oauth2",
                //            Name = "Bearer",
                //            In = ParameterLocation.Header,

                //        },
                //        new string[] { }
                //    }
                //});

                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.ApiKey,
                //    Flows = new OpenApiOAuthFlows
                //    {
                //        AuthorizationCode = new OpenApiOAuthFlow
                //        {
                //            AuthorizationUrl = new Uri("https://localhost:6001/connect/authorize"),
                //            TokenUrl = new Uri("https://localhost:6001/connect/token"),
                //            Scopes = new Dictionary<string, string>
                //            {
                //                {"ScopeWebApi", "WebApi Client"}
                //            }
                //        }
                //    }
                //});
            });


            services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:6001";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = "https://localhost:6001/resources"
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1");

                    // Additional OAuth settings (See https://github.com/swagger-api/swagger-ui/blob/v3.10.0/docs/usage/oauth2.md)
                    c.OAuthClientId("WebApi");
                    c.OAuthClientSecret("ClientSecret");
                    c.OAuthAppName("Web Client");
                    c.OAuthScopeSeparator(" ");
                    c.OAuthScopes("Api");
                    c.OAuthUsePkce();
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}