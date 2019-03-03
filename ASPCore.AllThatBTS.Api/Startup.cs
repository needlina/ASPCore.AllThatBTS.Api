using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Filter;
using ASPCore.AllThatBTS.Api.Middleware;
using ASPCore.AllThatBTS.Api.Model;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NLog.Time;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using System.Text;

namespace ASPCore.AllThatBTS.Api
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
            // Validators
            //services.AddSingleton<IValidator<RequestBoardListM>, BoardValidator>();

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = (context) =>
            //    {
            //        var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
            //        var result = new
            //        {
            //            Code = "00000",
            //            Message = "Validation errors",
            //            Errors = errors
            //        };
            //        return new BadRequestObjectResult(result);
            //    };
            //});


            TimeSource.Current = new CustomTimeZoneTimeSource()
            {
                Zone = "Korea Standard Time"
            };

            // AutoMapper Add Profile
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddFluentValidation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "All That BTS API", Version = "v1" });
                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            var key = Encoding.ASCII.GetBytes(AppConfiguration.JwtSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();

            // Global Error Handling + NLog.Logger
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "All That BTS API V1");
                c.RoutePrefix = "api/swagger";
                c.DisplayOperationId();
            });
        }
    }
}
