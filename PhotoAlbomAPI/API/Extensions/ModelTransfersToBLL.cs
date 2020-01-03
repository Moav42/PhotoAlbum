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

        public static CommentBLL Transform(this CommentModel model)
        {
            var comment = new CommentBLL
            {
                Id = model.Id,
                PostId = model.PostId,
                UserId = model.UserId,
                Text = model.Text,
                AddingDate = model.AddingDate
            };
            return comment;
        }

        public static CommentModel Transform(this CommentBLL commentBLL)
        {
            var comment = new CommentModel
            {
                Id = commentBLL.Id,
                PostId = commentBLL.PostId,
                UserId = commentBLL.UserId,
                Text = commentBLL.Text,
                AddingDate = commentBLL.AddingDate
            };
            return comment;
        }

        public static PostRateModel Transform(this PostRateBLL postRateBLL)
        {
            var postRate = new PostRateModel
            {
                PostId = postRateBLL.PostId,
                UserId = postRateBLL.UserId,
                Licked = postRateBLL.Licked
            };
            return postRate;
        }

        public static PostRateBLL Transform(this PostRateModel postRate)
        {
            var postRateBLL = new PostRateBLL
            {
                PostId = postRate.PostId,
                UserId = postRate.UserId,
                Licked = postRate.Licked
            };
            return postRateBLL;
        }

        public static OrganisationModel Transform(this OrganisationBLL modelBLL)
        {
            var model = new OrganisationModel
            {
                Id = modelBLL.Id,
                Name = modelBLL.Name,
                Location = modelBLL.Location
            };
            return model;
        }

        public static OrganisationBLL Transform(this OrganisationModel model)
        {
            var modelBLL = new OrganisationBLL
            {
                Id = model.Id,
                Name = model.Name,
                Location = model.Location
            };
            return modelBLL;
        }

    }
}
