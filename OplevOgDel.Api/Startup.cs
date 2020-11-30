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
using OplevOgDel.Api.Models.Configuration;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
        private SecretOptions secretsOptions { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // add DB connection string
            var connectionString = Configuration.GetConnectionString("OplevOgDelDb");
            // create a db connection for EF to use using the connection string
            services.AddDbContext<OplevOgDelDbContext>(x => x.UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure()));

            // initialize auto mapper library
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // initialize kisslogger library
            services.AddScoped((context) =>
            {
                return Logger.Factory.Get();
            });

            // configured our controllers to ignore reference loops when converting object to json
            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // get options from appsettings.json initialized into options classes
            swaggerOptions = Configuration.GetSection(SwaggerOptions.Swagger).Get<SwaggerOptions>();
            secretsOptions = Configuration.GetSection(SecretOptions.Secret).Get<SecretOptions>();

            // configure and set up swagger documentation generation
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

                // tell it to use our XML documentation on the code files
                var filepath = Path.Combine(AppContext.BaseDirectory, "OplevOgDel.Api.xml");
                c.IncludeXmlComments(filepath);

            });

            // add our repository and configuration options classes to the dependency injection container
            services.AddScoped<IExperienceRepository, ExperienceRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IReviewReportRepository, ReviewReportRepository>();
            services.AddScoped<IExperienceReportRepository, ExperienceReportRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPictureRepository, PictureRepository>();
            services.Configure<FileUploadOptions>(Configuration.GetSection(FileUploadOptions.FileUpload));
            services.Configure<SecretOptions>(Configuration.GetSection(SecretOptions.Secret));

            // configure authentication
            services.AddAuthentication(x =>
            {
                // we want to use the bearer authentication scheme
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => 
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretsOptions.Signature)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime  = true,
                    ValidIssuer = secretsOptions.Issuer,
                    ValidAudience = secretsOptions.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            });
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
                // if an error happens in production, return /error result
                app.UseExceptionHandler("/error");
            }

            // use swagger and swagger auto generated UI
            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint(swaggerOptions.Endpoint, swaggerOptions.Name);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
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
