using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeTest.Context;
using CodeTest.Interfaces;
using CodeTest.Persistence;
using CodeTest.Repository;
using CodeTest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CodeTest
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddControllers();
            // Configure the persistence in another layer
            MongoDbPersistence.Configure();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                    Version = "v1",
                    Title = "Workiom Code Test",
                    Description = "Workiom Code Test"
                    });
            });
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                          .CreateScope())
                {
                    ISeedDbService seed= serviceScope.ServiceProvider.GetService<ISeedDbService>();
                    seed.Seed();
                } 
                
            }

            app.UseHttpsRedirection();
            //app.UseMvc();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Workiom Code Test v1.0");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            #region Context
            services.AddScoped<IMongoContext, MongoContext>();
            #endregion
            #region Repositories
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IAddedColumnRepository, AddedColumnRepository>();
            services.AddScoped<ISequenceRepository, SequenceRepository>();
            #endregion
            #region Services
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IAddedColumnService, AddedColumnService>();
            services.AddScoped<ISeedDbService, SeedDbService>();
            #endregion
        }
    }
}
