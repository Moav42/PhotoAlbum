using DAL.Entities;
using BLL.Models;
using AutoMapper;

namespace BLL.Maping
{
    /// <summary>
    /// Class containing profile for configuration AutoMapper
    /// </summary>
    public class MapingProfiles : Profile
    {
        public MapingProfiles()
        {
            CreateMap<Category, CategoryBLL>();
            CreateMap<CategoryBLL, Category>();
            CreateMap<Comment, CommentBLL>();
            CreateMap<CommentBLL, Comment>();
            CreateMap<Organisation, OrganisationBLL>();
            CreateMap<OrganisationBLL, Organisation>();
            CreateMap<Post, PostBLL>();
            CreateMap<PostBLL, Post>();
            CreateMap<PostRate, PostRateBLL>();
            CreateMap<PostRateBLL, PostRate>();
            CreateMap<Tag, TagBLL>();
            CreateMap<TagBLL, Tag>();
        }
    }
}
