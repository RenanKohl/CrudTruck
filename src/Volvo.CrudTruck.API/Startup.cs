using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volvo.CrudTruck.API.Helpers;
using Volvo.CrudTruck.Data.Context;

namespace Volvo.CrudTruck.API
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
            services.AddSwaggerConfiguration();
            services.AddHttpContextAccessor();
            services.AddJwtConfiguration(Configuration);
            services.AddCors();
            services.RegisterDependencies();
            services.AddDbContext<DatabaseContext>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
        {
            seedingService.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyMethod()
                            .AllowAnyOrigin()
                            .AllowAnyHeader()                       
                        );

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                var clientConfig = Configuration.GetSection("AuthConfiguration").GetSection("Client");
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CrudTruck");
                options.RoutePrefix = clientConfig.GetValue<string>("RoutePrefix");
                options.OAuthClientId(clientConfig.GetValue<string>("OAuthClientId"));
                options.OAuthAppName(clientConfig.GetValue<string>("OAuthAppName"));
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

            //app.UseSpa(spa =>
            //{
            //    spa.Options.SourcePath = "wwwroot";
            //    if (env.IsDevelopment())
            //        spa.UseProxyToSpaDevelopmentServer(Configuration.GetValue<string>("AngularCliDeveloperServer"));
            //});
        }
    }
}
