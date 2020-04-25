using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ChatApp.Api.IoC;
using ChatApp.Bl.IoC;
using ChatApp.Core.IoC;
using ChatApp.Model.Contexts.ChatApp;
using ChatApp.Model.IoC;
using AutoMapper;
using System;
using Swashbuckle.AspNetCore.Swagger;
using ChatApp.Core.Models;
using ChatApp.Api.Filters;
using FluentValidation.AspNetCore;
using ChatApp.Bl.Validators;
using ChatApp.Api.Config;
using ChatApp.Api.Hubs;

namespace ChatApp.Api
{
    public class Startup
    {
        private AzureAdB2C AzureB2CSettings { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region AuthConfig

            #endregion


            #region CORS
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllPolicy",
                      builder =>
                      {
                          builder
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials()
                                 .AllowAnyOrigin()
                                .WithOrigins(appSettings.ClientUrls)
                                 ;
                      });
            });
            #endregion

            #region IoC Registry
            services.AddCoreRegistry();
            services.AddModelRegistry();
            services.AddBlRegistry();
            services.AddApiRegistry();
            #endregion



            #region ContextConfiguration
            string myAppDbContextConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ChatAppDbContext>(op => op.UseSqlServer(myAppDbContextConnection), ServiceLifetime.Transient);
            #endregion

            //Register Serilog from extension
            services.AddSerilog(Configuration);


            #region Global Api Config
            services.AddMvc(o=> {
                    o.Filters.Add<ValidationHttpParametersFilter>();
                
            })
               .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<KeyValueValidator>());
            #endregion


            #region SignalR
                services.AddSignalR();
            #endregion
            #region Adding Settings Sections
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<SerilogSettings>(Configuration.GetSection("SerilogSettings"));
            services.Configure<AzureAdB2C>(Configuration.GetSection("AzureAdB2C"));
            #endregion

            #region Adding External Libs
            //Register AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ChatApp API", Version = "v1" });
            });
            #endregion

            Dependency.ServiceProvider = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatApp API V1");
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseCors("AllowAllPolicy");
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatAppHub>("/chatHub");
            });
            app.UseMvc();
        }

    }
}
