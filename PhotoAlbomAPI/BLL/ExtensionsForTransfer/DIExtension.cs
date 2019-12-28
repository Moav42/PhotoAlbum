using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ExtensionsForTransfer
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
            return services;
        }
    }
}
