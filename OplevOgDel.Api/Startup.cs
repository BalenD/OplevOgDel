using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OplevOgDel.Api.Data;
using OplevOgDel.Api.Services;
using OplevOgDel.Api.Services.RepositoryBase;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Diagnostics;
using Microsoft.OpenApi.Models;
using OplevOgDel.Api.Models.configuration;

namespace OplevOgDel.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private SwaggerOptions swaggerOptions { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("OplevOgDelDb");
            services.AddDbContext<OplevOgDelDbContext>(x => x.UseSqlServer(connectionString));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped((context) =>
            {
                return Logger.Factory.Get();
            });

            services.AddControllers();

            swaggerOptions = Configuration.GetSection(SwaggerOptions.Swagger).Get<SwaggerOptions>();

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc(swaggerOptions.Version, new OpenApiInfo
                {
                    Version = swaggerOptions.Version,
                    Title = swaggerOptions.Title,
                    Description = swaggerOptions.Description,
                    TermsOfService = new Uri(swaggerOptions.TermsOfService),
                    Contact = new OpenApiContact
                    {
                        Name = swaggerOptions.ContactName,
                        Email = swaggerOptions.ContactEmail,
                        Url = new Uri(swaggerOptions.ContactUrl)
                    },
                    License = new OpenApiLicense
                    {
                        Name = swaggerOptions.LicenseName,
                        Url = new Uri(swaggerOptions.LicenseUrl)
                    }

                });
            });

            services.AddScoped<IExperienceRepository, ExperienceRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint(swaggerOptions.Endpoint, swaggerOptions.Name);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseKissLogMiddleware(options => {
                ConfigureKissLog(options);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureKissLog(IOptionsBuilder options)
        {
            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();
 
                    if (ex is NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }
 
                    return sb.ToString();
                });
 
            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };
 
            // register logs output
            RegisterKissLogListeners(options);
        }
 
        private void RegisterKissLogListeners(IOptionsBuilder options)
        {
            var organizationId = Configuration["KissLog.OrganizationId"];
            var applicationId = Configuration["KissLog.ApplicationId"];
            var apiUrl = Configuration["KissLog.ApiUrl"];

            // register KissLog.net cloud listener
            options.Listeners.Add(new RequestLogsApiListener(new Application(organizationId, applicationId))
            {
                ApiUrl = apiUrl
            });;
        }
    }
}
