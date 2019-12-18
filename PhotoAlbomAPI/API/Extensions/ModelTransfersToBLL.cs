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
    }
}
