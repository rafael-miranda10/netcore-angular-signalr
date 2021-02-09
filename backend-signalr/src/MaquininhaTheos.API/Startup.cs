using MaquininhaTheos.API.Configuration;
using MaquininhaTheos.Data.Context;
using MaquininhaTheos.Domain.Servicos;
using MaquininhaTheos.Domain.TheosHub;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace MaquininhaTheos.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<MaquininhaTheosContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(options =>
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:4200")
                        .AllowCredentials()));

            services.AddSignalR();

            services.ResolveDependencies();
            services.AddHostedService<DashboardHostedService>();
            services.AddHostedService<TesteHostedService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            //app.UseCors(options =>
            //   options
            //       .AllowAnyHeader()
            //       .AllowAnyMethod()
            //       .AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TheosMaquininhaHub>("/theoshub");
            });
        }
    }
}
       