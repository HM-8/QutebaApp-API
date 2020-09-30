using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace QutebaApp_API
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

            string path = @"C:\Users\Medhin\Desktop\HIWOT\WorkSpace\QutebaAPP Project\Documents\service-account-file-qutebaapp.json";

            var app = FirebaseApp.Create(new AppOptions()
            {
                ProjectId = Configuration.GetValue<string>("Firebase:project_id"),
                Credential = GoogleCredential.FromFile(path)
            });

            Console.WriteLine($"DEFAULT >>>> {FirebaseApp.DefaultInstance.Name}");
            Console.WriteLine($"APP >>>> {app.Options.ProjectId}");

            services.AddSwaggerGen();
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

            app.UseSwagger();
            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "QutebaApp-API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
