using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parky.API.Data;
using Microsoft.EntityFrameworkCore;
using Parky.API.Repository.IRepository;
using Parky.API.Repository;
using Parky.API.Mapper;
using System.Reflection;
using System.IO;

namespace Parky.API
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
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<INationalParkRepository, NationalParkRepository>();
            services.AddScoped<ITrialRepository, TrialRepository>();
            services.AddAutoMapper(typeof(MapConfigurations));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("ParkyOpenAPISpecNP", new OpenApiInfo
                {
                    Title = "Parky.API (National Park)",
                    Version = "v1",
                    Description = "This is basic Parky API NP",
                    Contact = new OpenApiContact
                    {
                        Email = "ersngun@gmail.com",
                        Name = "Ersin Gun",
                        Url = new Uri("https://www.google.com"),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT Licence",
                        Url = new Uri("https://www.google.com")
                    }
                });

                c.SwaggerDoc("ParkyOpenAPISpecTrails", new OpenApiInfo
                {
                    Title = "Parky.API (Trails)",
                    Version = "v1",
                    Description = "This is basic Parky API Trails",
                    Contact = new OpenApiContact
                    {
                        Email = "ersngun@gmail.com",
                        Name = "Ersin Gun",
                        Url = new Uri("https://www.google.com"),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT Licence",
                        Url = new Uri("https://www.google.com")
                    }
                });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                c.IncludeXmlComments(cmlCommentsFullPath);
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
                    c.SwaggerEndpoint("/swagger/ParkyOpenAPISpecNP/swagger.json", "Parky.API NP");
                    c.SwaggerEndpoint("/swagger/ParkyOpenAPISpecTrails/swagger.json", "Parky.API Trails");
            });

                app.UseRouting();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }
}

