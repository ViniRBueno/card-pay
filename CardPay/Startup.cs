using CardPay.Entities;
using CardPay.Interfaces;
using CardPay.Jwt;
using CardPay.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace CardPay
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
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IFamilyService), typeof(FamilyService));
            services.AddTransient(typeof(ILoanService), typeof(LoanService));

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddWkhtmltopdf();
            var key = Encoding.ASCII.GetBytes(TokenManager.GetSecret());
            services.AddCors(options =>
            {
                //remover
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                .WithOrigins("http://localhost:3000", "http://127.0.0.1:8080", "https://localhost:44309"));
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if(!string.IsNullOrWhiteSpace(context.Request.Cookies["access-token"]))
                            context.Token = context.Request.Cookies["access-token"];
                        if(!string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"]))
                            context.Token = context.Request.Headers["Authorization"];
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddDbContext<CardPayContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), provideroptions => provideroptions.CommandTimeout(60));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                app.UseSwagger();
                app.UseSwaggerUI();
            });
        }
    }
}
