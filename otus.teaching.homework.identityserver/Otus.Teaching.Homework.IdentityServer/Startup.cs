// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            });

            builder.AddTestUsers(TestUsers.Users);
            builder.AddInMemoryIdentityResources(Config.IdentityResources);
            builder.AddInMemoryApiScopes(Config.ApiScopes);
            builder.AddInMemoryApiResources(Config.ApiResources);
            builder.AddInMemoryClients(Config.Clients);
            builder.AddDeveloperSigningCredential();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddIdentityServerAuthentication(c =>
                {
                    c.Authority = "http://localhost:6001/auth-server/";
                    c.RequireHttpsMetadata = false;
                    c.ApiName = "Api";
                });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:7777/signin-google    
                    options.ClientId = "723151777522-7dt2ti6b2oq9hmrem9c2obchks6ildqs.apps.googleusercontent.com";
                    options.ClientSecret = "70-8sb0wk6_9xtNM1HtQdx45";
                });

            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            
                    // register your IdentityServer with Facebook at https://developers.facebook.com/apps
            
                    options.ClientId = "1093722324472497";
                    options.ClientSecret = "7b05326ed4dcfcb4825b7723156b4c59";
                    options.Scope.Add("email");
                    options.Scope.Add("public_profile");
                    options.Scope.Add("user_age_range");
                });

            // services.AddAuthentication()
            //     .AddGoogle(options =>
            //     {
            //         options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //         options.ClientId = Configuration.GetValue<string>("GoogleClientId");
            //         options.ClientSecret = Configuration.GetValue<string>("GoogleClientSecret");
            //     })
            //     .AddFacebook(options =>
            //     {
            //         options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //         options.AppId = Configuration.GetValue<string>("FacebookAppId");
            //         options.AppSecret = Configuration.GetValue<string>("FacebookAppSecret");
            //     });

        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}