using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ExtensionsForTransfer
{
    static class ModelTransfersToDAL
    {
        public static Post Transform(this PostBLL postBLL)
        {
            var post = new Post
            {
                Id = postBLL.Id,
                Title = postBLL.Title,
                LocationPath = postBLL.LocationPath,
                UserId = postBLL.UserId,
                AddingDate = postBLL.AddingDate

            };
            return post;
        }

        public static PostBLL Transform(this Post postBLL)
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

        public static Category Transform(this CategoryBLL categoryBLL)
        {
            var category = new Category
            {
                Id = categoryBLL.Id,
                Title = categoryBLL.Title,
                Description = categoryBLL.Description
            };
            return category;
        }

        public static CategoryBLL Transform(this Category category)
        {
            var categoryBLL = new CategoryBLL
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description
            };
            return categoryBLL;
        }

        public static Comment Transform(this CommentBLL commentBLL)
        {
            var comment = new Comment
            {
                Id = commentBLL.Id,
                PostId = commentBLL.PostId,
                UserId = commentBLL.UserId,
                Text = commentBLL.Text,
                AddingDate = commentBLL.AddingDate
            };
            return comment;
        }

        public static CommentBLL Transform(this Comment commentBLL)
        {
            var comment = new CommentBLL
            {
                Id = commentBLL.Id,
                PostId = commentBLL.PostId,
                UserId = commentBLL.UserId,
                Text = commentBLL.Text,
                AddingDate = commentBLL.AddingDate
            };
            return comment;
        }

        public static Tag Transform(this TagBLL tagBLL)
        {
            var tag = new Tag
            {
                Id = tagBLL.Id,
                Title = tagBLL.Title
            };
            return tag;
        }

        public static TagBLL Transform(this Tag tagDAL)
        {
            var tagBLL = new TagBLL
            {
                Id = tagDAL.Id,
                Title = tagDAL.Title
            };
            return tagBLL;
        }
        public static PostRate Transform(this PostRateBLL postRateBLL)
        {
            var postRate = new PostRate
            {
                PostId = postRateBLL.PostId,
                UserId = postRateBLL.UserId,
                Licked = postRateBLL.Licked
            };
            return postRate;
        }
        public static PostRateBLL Transform(this PostRate postRateDAL)
        {
            var postRateBLL = new PostRateBLL
            {
                PostId = postRateDAL.PostId,
                UserId = postRateDAL.UserId,
                Licked = postRateDAL.Licked
            };
            return postRateBLL;
        }
    }
}
