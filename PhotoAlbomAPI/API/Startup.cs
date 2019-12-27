using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.JWT;
using API.Models;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using API.Extensions;

namespace API
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DAL.EF.DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.AddScoped<ITagService<TagBLL>, TagService>();
            services.AddScoped<ICategoryService<CategoryBLL>, CategoryService>();
            services.AddScoped<IPostService<PostBLL>, PostService>();
            services.AddScoped<IOrganisationService<OrganisationBLL>, OrganisationService>();

            services.AddJWT(_signingKey, Configuration);
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "admin"));
                options.AddPolicy("Organisation", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "organisation"));
                options.AddPolicy("Moderator", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "moderator"));
                options.AddPolicy("User", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "user"));
            });

            var builder = services.AddIdentityCore<DAL.Entities.User> (o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);

            builder.AddEntityFrameworkStores<DAL.EF.DbContext>().AddDefaultTokenProviders().AddRoles<IdentityRole>();

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));

            app.UseHttpsRedirection();
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
