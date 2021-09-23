using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Billdeer.DataAccess.Concrete.EntityFramework.Contexts;
using Billdeer.Core.Extensions;
using Billdeer.Business;
using Billdeer.Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Billdeer.Core.Utilities.Security.Encryption;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Billdeer.Core.Utilities.IoC;
using Billdeer.Core.DependencyResolvers;
using Billdeer.Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using System.Text.Json.Serialization;

namespace Billdeer.WebAPI
{
    public class Startup : BusinessStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
<<<<<<< Updated upstream

            services.AddControllers()

                            .AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                                options.JsonSerializerOptions.IgnoreNullValues = true;
                            });
            services.AddOptions();

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });


            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule(),
            });

            services.AddDbContext<BilldeerDbContext>();

            // AutoMapper ve MediatR BusinessStartup kurulduðunda oraya taþýnacak.
            services.AddAutoMapper(typeof(BusinessStartup).Assembly);
            services.AddMediatR(typeof(BusinessStartup).Assembly);
=======
>>>>>>> Stashed changes


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Billdeer.WebAPI", Version = "v1" });
            });
            services.AddBusinessRegistration();
            //services.AddTransient<IEntityExampleRepository, EntityExampleRepository>();

            services.AddDbContext<BilldeerDbContext>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Billdeer.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCostumExceptionHandler();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
