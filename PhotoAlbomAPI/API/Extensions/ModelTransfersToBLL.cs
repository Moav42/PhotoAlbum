using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;
using API.Models;

namespace API.Extensions
{
    public static class ModelTransfersToBLL
    {
        public static TagBLL Transform(this TagModel tagModel)
        {
            var tag = new TagBLL
            {
                Id = tagModel.Id,
                Title = tagModel.Title
            };
            return tag;
        }
        public static TagModel Transform(this TagBLL tagBLL)
        {
            var tag = new TagModel
            {
                Id = tagBLL.Id,
                Title = tagBLL.Title
            };
            return tag;
        }
        public static CategoryModel Transform(this CategoryBLL categoryBLL)
        {
            var category = new CategoryModel
            {
                Id = categoryBLL.Id,
                Title = categoryBLL.Title,
                Description = categoryBLL.Description
            };
            return category;
        }

        public static CategoryBLL Transform(this CategoryModel category)
        {
            var categoryBLL = new CategoryBLL
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description
            };
            return categoryBLL;
        }
        public static PostModel Transform(this PostBLL postBLL)
        {
            var post = new PostModel
            {
                Id = postBLL.Id,
                Title = postBLL.Title,
                LocationPath = postBLL.LocationPath,
                UserId = postBLL.UserId,
                AddingDate = postBLL.AddingDate

            };
            return post;
        }

        public static PostBLL Transform(this PostModel postBLL)
        {
            var post = new PostBLL
            {
                Id = postBLL.Id,
                Title = postBLL.Title,
                LocationPath = postBLL.LocationPath,
                UserId = postBLL.UserId,
                AddingDate = postBLL.AddingDate

            };
            return post;
        }
    }
}
