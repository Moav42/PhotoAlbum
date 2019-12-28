using BLL.Interfaces;
using BLL.JWT;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Extensions
{
    public static class DIExtension
    {
        public static IServiceCollection AddBllServices(this IServiceCollection services)
        {
            services.AddScoped<ITagService<TagBLL>, TagService>();
            services.AddScoped<ICategoryService<CategoryBLL>, CategoryService>();
            services.AddScoped<IPostService<PostBLL>, PostService>();
            services.AddScoped<IOrganisationService<OrganisationBLL>, OrganisationService>();
            services.AddScoped<ICommentService<CommentBLL>, CommentService>();
            services.AddScoped<IPostRateService<PostRateBLL>, PostRateService>();
            services.AddScoped<IAccountService<UserBLL>, AccountService>();
            services.AddScoped<IAuthorizationService<UserBLL>, AuthorizationService>();
            services.AddScoped<IAccountManagerService<UserBLL>, AccountManagerService>();

            return services;
        }

        public static IServiceCollection AddJWTAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "admin"));
                options.AddPolicy("Organisation", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "organisation"));
                options.AddPolicy("Moderator", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "moderator"));
                options.AddPolicy("User", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, "user"));
            });

            return services;
        }

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
    }
}
