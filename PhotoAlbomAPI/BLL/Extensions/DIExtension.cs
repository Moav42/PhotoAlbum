using BLL.Interfaces;
using BLL.JWT;
using BLL.Models;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BLL.Extensions
{
    /// <summary>
    /// Static class, aggregating extension methods for dependency injection
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// Configures dependency injection for UOF and application business logic services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITagService<TagBLL>, TagService>();
            services.AddScoped<ICategoryService<CategoryBLL>, CategoryService>();
            services.AddScoped<IPostService<PostBLL>, PostService>();
            services.AddScoped<IOrganisationService<OrganisationBLL>, OrganisationService>();
            services.AddScoped<ICommentService<CommentBLL>, CommentService>();
            services.AddScoped<IPostRateService<PostRateBLL>, PostRateService>();
            services.AddScoped<IAccountService<UserBLL>, AccountService>();
            services.AddScoped<IAuthorizationService<UserBLL>, AuthorizationService>();
            services.AddScoped<IAccountManagerService<UserBLL>, AccountManagerService>();        
            services.AddSingleton<IJwtFactory, JwtFactory>();

            return services;
        }

        /// <summary>
        /// Configures dependency injection for context settings, adds a data provider and configures its by connection string.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration">Application settings passed from the presentation layer</param>
        /// <returns></returns>
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DAL.EF.DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        /// <summary>
        /// Configures JWT Authorization, adds support for four access levels to the functionality of the service, based on roles of user.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddJWTAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "admin"));
                options.AddPolicy("Moderator", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "admin", "moderator"));
                options.AddPolicy("Organisation", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "organisation", "moderator", "admin"));
                options.AddPolicy("AllUsers", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "organisation", "moderator", "admin", "user"));
            });

            return services;
        }

        /// <summary>
        /// Adds and configures the identity system for application
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<DAL.Entities.User>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<DAL.EF.DbContext>().AddDefaultTokenProviders().AddRoles<IdentityRole>();

            return services;
        }

        /// <summary>
        /// Adds and configures the swwagger service support for application
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
            });

            return services;
        }
    }
}
