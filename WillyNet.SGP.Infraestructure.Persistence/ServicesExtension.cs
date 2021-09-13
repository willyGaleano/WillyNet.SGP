using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Interfaces;
using WillyNet.SGP.Core.Application.Wrappers;
using WillyNet.SGP.Core.Domain.Entities;
using WillyNet.SGP.Core.Domain.Settings;
using WillyNet.SGP.Infraestructure.Persistence.Contexts;
using WillyNet.SGP.Infraestructure.Persistence.Repository;
using WillyNet.SGP.Infraestructure.Persistence.Services;
using WillyNet.SGP.Infraestructure.Persistence.Services.Authenticate;

namespace WillyNet.SGP.Infraestructure.Persistence
{
    public static class ServicesExtension
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            services.AddDbContext<DbSGPContext>(options => options.UseSqlServer(
               configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly(typeof(DbSGPContext).Assembly.FullName)));
            #endregion

            #region Identity
            services.AddIdentity<UserApp, IdentityRole>().AddEntityFrameworkStores<DbSGPContext>().AddDefaultTokenProviders();
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                    };

                    o.Events = new JwtBearerEvents()
                    {   
                        //OnMessageReceived

                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("Usted no esta autorizado"));
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 400;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("Usted no tiene permisos sobre este recurso"));
                            return context.Response.WriteAsync(result);
                        }
                    };

                });
            #endregion

            #region Repositories
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            #endregion

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransactionDb, TransactionDb>();
            #endregion

            #region Caching
            /*
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("Caching:RedisConnection");
            });
            */
            #endregion
        }
    }
}
