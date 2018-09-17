using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using log4net.Extensions.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NJsonSchema;
using NSwag.AspNetCore;
using ParadiseExplorer.Domains;
using ParadiseExplorer.Profiles;
using ParadiseExplorer.Services;

//using ParadiseExplorer.Domains;

namespace ParadiseExplorer
{
    public class Startup
    {
        private string[] CorsOrigins { get; set; }
        private string DefaultPolicyName = "localhost";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            CorsOrigins = configuration["App:CorsOrigins"]
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            BsonMapping.MapModels();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultPolicyName, builder =>
                {
                    // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                    builder
                        .WithOrigins(CorsOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connection = @"Server=localhost;Database=Paradise;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<ParadiseContext>(options => options.UseSqlServer(connection));
            services.AddTransient<IMongoClient, MongoClient>(provider => new MongoClient("mongodb://localhost:27017"));
            //services.AddTransient<IParadiseService, MongoParadiseService>();
            services.AddTransient<IParadiseService, SqlParadiseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net(); // << Add this line
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(DefaultPolicyName); // Enable CORS!
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });

            app.UseMvc();
        }
    }
}
