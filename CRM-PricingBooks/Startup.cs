using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using CRM_PricingBooks.BusinessLogic;
using CRM_PricingBooks.Database;
using Services;
using Microsoft.OpenApi.Models;

namespace CRM_PricingBooks
{
    public class Startup
    {
        const string SWAGGER_SECTION_SETTING_KEY = "SwaggerSettings";
        const string SWAGGER_SECTION_SETTING_TITLE_KEY = "Title";
        const string SWAGGER_SECTION_SETTING_VERSION_KEY = "Version";

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {   
            services.AddControllers();

            services.AddTransient<IPriceLogic,PriceLogic>();
            services.AddSingleton<IPricingBookDBManager, PricingBookDBManager>();

            services.AddTransient<IProductLogic, ProductLogic>();
            services.AddTransient<ICampaignBackingService, CampaignBackingService>();
            //services.AddTransient<ICampaignBackingService, CampaignBackingService>();

            // ADDING CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.WithOrigins("*")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      );
            });
            // End CORS block

            //Add Swagger
            var swaggerTitle = Configuration
                .GetSection(SWAGGER_SECTION_SETTING_KEY)
                .GetSection(SWAGGER_SECTION_SETTING_TITLE_KEY);
            var swaggerVersion = Configuration
                .GetSection(SWAGGER_SECTION_SETTING_KEY)
                .GetSection(SWAGGER_SECTION_SETTING_VERSION_KEY);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc
                (swaggerVersion.Value, 
                new Microsoft.OpenApi.Models.OpenApiInfo() 
                    {
                        Title = swaggerTitle.Value, 
                        Version = swaggerVersion.Value
                    }
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var swaggerTitle = Configuration
                .GetSection(SWAGGER_SECTION_SETTING_KEY)
                .GetSection(SWAGGER_SECTION_SETTING_TITLE_KEY);
            var swaggerVersion = Configuration
                .GetSection(SWAGGER_SECTION_SETTING_KEY)
                .GetSection(SWAGGER_SECTION_SETTING_VERSION_KEY);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerVersion.Value}/swagger.json", swaggerTitle.Value);
            });
        }
    }
}
