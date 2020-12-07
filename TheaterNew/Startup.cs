using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Theater.Domain.Core.Entities;
using Theater.Domain.Interfaces;
using Theater.Infrastructure.Business.Services;
using Theater.Infrastructure.Data;
using Theater.Infrastructure.Data.Repositories;
using Theater.Mappings;
using Theater.Services.Interfaces;
using Theater.Domain.Core.DTO;

namespace TheaterNew
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
            services.AddDbContext<TheaterContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<TheaterContext>();

            services.AddControllers();
                        
            services.AddScoped<DbContext, TheaterContext>();
            services.AddScoped<IBaseRepository<Performance>, BaseRepository<Performance>>();
            services.AddScoped<IBaseRepository<Actor>, BaseRepository<Actor>>();
            services.AddScoped<IBaseRepository<ActorRole>, BaseRepository<ActorRole>>();
            services.AddScoped<IBaseRepository<Poster>, BaseRepository<Poster>>();
            services.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();
            
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBaseService<ActorDTO>, ActorService>();
            services.AddTransient<IBaseService<ActorRoleDTO>, ActorRoleService>();
            services.AddTransient<IBaseService<PerformanceDTO>, PerformanceService>();
            services.AddTransient<IBaseService<PosterDTO>, PosterService>();
            services.AddTransient<IBaseService<RoleDTO>, RoleService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Theater");
                c.RoutePrefix = string.Empty;
            });

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
