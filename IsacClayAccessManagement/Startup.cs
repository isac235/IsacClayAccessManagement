namespace IsacClayAccessManagement
{
    using System;
    using System.Text;
    using Data.Repository;
    using Data.Repository.Interfaces;
    using Data.Repository.Repositories;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Serilog;
    using Services;
    using Services.Interfaces;

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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Specify the issuer (who created and signed the token)
                    ValidIssuer = this.Configuration.GetSection("Authorization:Issuer").Value,

                    // Specify the audience (who the token is intended for)
                    ValidAudience = this.Configuration.GetSection("Authorization:Audience").Value,

                    // Specify the signing key to validate the token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration.GetSection("Authorization:SigningKey").Value)),

                    // Specify the validation type
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,

                    // Specify whether the token should be validated for expiration
                    ValidateLifetime = true,
                };
            });

            services.AddAuthorization();

            services.AddControllers(); //

            services.AddDbContext<OfficesAccessDbContext>(options =>
            {
                options.UseMySql(this.Configuration.GetSection("Database:ConnectionString").Value, new MySqlServerVersion(new Version(8, 3, 0)));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IOfficeService, OfficeService>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IDoorRepository, DoorRepository>();
            services.AddScoped<IDoorService, DoorService>();

            services.AddScoped<IScopeRepository, ScopeRepository>();
            services.AddScoped<IScopeService, ScopeService>();

            services.AddScoped<IUserRoleMappingRepository, UserRoleMappingRepository>();
            services.AddScoped<IUserRoleMappingService, UserRoleMappingService>();

            services.AddScoped<IAccessEventRepository, AccessEventRepository>();
            services.AddScoped<IAccessEventService, AccessEventService>();

            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IUserClaimService, UserClaimService>();

            services.AddScoped<IClaimRepository, ClaimRepository>();
            services.AddScoped<IClaimService, ClaimService>();

            // Add Serilog
            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
                //etc
            });
            //services.AddLogging(loggingBuilder =>
            //{
            //    loggingBuilder.AddSerilog(CreateLogger());
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); //
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private Serilog.ILogger CreateLogger()
        {
            var logFilePath = Configuration.GetSection("Logging:Path").Value; ;

            return new LoggerConfiguration()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day) // Log to a file with rolling
                .CreateLogger();
        }
    }
}
