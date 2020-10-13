using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QutebaApp_Core.Services.Implementations;
using QutebaApp_Core.Services.Interfaces;
using QutebaApp_Data.Data;

namespace QutebaApp_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<QutebaAppDbContext>(d => d.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFirebaseService, FirebaseService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            string path = @"C:\Users\Medhin\Desktop\HIWOT\WorkSpace\QutebaAPP Project\Documents\service-account-file-qutebaapp.json";

            var app = FirebaseApp.Create(new AppOptions()
            {
                ProjectId = Configuration.GetValue<string>("Firebase:project_id"),
                Credential = GoogleCredential.FromFile(path)
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetValue<string>("Firebase:issuer");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration.GetValue<string>("Firebase:issuer"),
                        ValidateAudience = true,
                        ValidAudience = Configuration.GetValue<string>("Firebase:project_id"),
                        ValidateLifetime = true,
                        ValidateActor = true
                    };
                    options.Validate();
                });

            services.AddAuthorization();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "QutebaApp API", Version = "v1" });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Insert Token with Bearer into the Field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "QutebaApp-API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
