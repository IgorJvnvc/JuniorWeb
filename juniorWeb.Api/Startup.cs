using JuniorWeb.DetaAccess;
using JuniorWeb.Api.CORE;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using JuniorWeb.Api.Errors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JuniorWeb.Api.Extensions;

namespace ASPNedelja3Vezbe.Api
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
            var settings = new AppSettings();

            Configuration.Bind(settings);

            services.AddSingleton(settings);
            services.AddApplicationUser();
            services.AddJwt(settings);
            services.AddVezbeDbContext();
            services.AddTransient<IExceptionLogger, ConsoleExceptionLogger>();
            services.AddControllers();
            services.AddHttpContextAccessor();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JuniorWeb.Api", Version = "v1" });

            //    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JuniorWeb.Api v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
