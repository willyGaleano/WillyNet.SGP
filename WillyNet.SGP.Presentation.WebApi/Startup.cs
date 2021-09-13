using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application;
using WillyNet.SGP.Core.Application.Interfaces;
using WillyNet.SGP.Infraestructure.Persistence;
using WillyNet.SGP.Infraestructure.Shared;
using WillyNet.SGP.Presentation.WebApi.Extensions;
using WillyNet.SGP.Presentation.WebApi.Services;

namespace WillyNet.SGP.Presentation.WebApi
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
            services.AddApplicationLayer();
            services.AddSharedInfraestructure(Configuration);
            services.AddPersistenceInfraestructure(Configuration);
            services.AddApiVersioningExtension();
            services.AddSwaggerExtension();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddControllers();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseErrorHandlingMiddleware();
            /*
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WillyNet.SGP.Presentation.WebApi v1"));
            }*/
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwaggerExtension();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
