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
using Microsoft.OpenApi.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend
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
            services.AddDbContext<CustomerDbContext>(options =>
                        options.UseNpgsql(Configuration.GetConnectionString("customerdb")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Configuration["MIGRATE"] == "y")
            {
                MigrateDatabase(app, seedDatabase: env.IsDevelopment());
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void MigrateDatabase(IApplicationBuilder app, bool seedDatabase)
        {
            System.Console.WriteLine("MigrateDatabase");
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CustomerDbContext>())
                {
                    context.Database.Migrate();

                    if (seedDatabase)
                    {
                        foreach (var name in new[] { "John", "Matt", "Omair", "Dan", "Radka", "Andrew", "Tom" })
                        {
                            if (!context.Customers.Any())
                            {
                                context.Customers.Add(new Customer { Name = name });
                            }
                        }
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
