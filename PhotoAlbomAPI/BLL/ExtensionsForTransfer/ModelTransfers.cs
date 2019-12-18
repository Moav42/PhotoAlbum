using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ExtensionsForTransfer
{
    static class ModelTransfers
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

        public static Tag Transform(this TagBLL tagBLL)
        {
            var tag = new Tag
            {
                Id = tagBLL.Id,
                Title = tagBLL.Title
            };
            return tag;
        }
    }
}
