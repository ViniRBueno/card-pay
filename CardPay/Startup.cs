using CardPay.Entities;
using CardPay.Interfaces;
using CardPay.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
