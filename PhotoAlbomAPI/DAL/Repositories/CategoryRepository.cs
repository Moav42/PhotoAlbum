using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository<Category>
    {
        private readonly DbContext DB;
        private readonly IPostCategoriesRepository<PostCategories> _postCategories;

        public CategoryRepository(DbContext context)
        {
            DB = context;
            _postCategories = new PostCategoriesRepository(context);
        }

        public void Create(Category item)
        {
            DB.Categories.Add(item);
        }

        public void Delete(int id)
        {
            Category category = DB.Categories.Find(id);
            if (category != null)
            {
                DB.Categories.Remove(category);
            }
        }

        public Category Read(int id)
        {
            return DB.Categories.Find(id);
        }

        public IEnumerable<Category> ReadAll()
        {
            return DB.Categories;
        }

        public void Update(Category item)
        {
            DB.Categories.Update(item);
        }

        public IEnumerable<Category> ReadAllByPost(int postId)
        {
            var postComments = _postCategories.ReadAll().Where(pt => pt.PostId == postId);
            var categories = new List<Category>();
            foreach (var item in postComments)
            {
                categories.Add(Read(item.PostId));
            }
            return categories;
        }

        public void AddTagToPost(int categoryId, int postId)
        {
            var postCat = new PostCategories { PostId = postId, CategoryId = categoryId };
            _postCategories.Create(postCat);
        }

        public void DeleteTagFromPost(int categoryId, int postId)
        {
            var postCat = new PostCategories { PostId = postId, CategoryId = categoryId };
            _postCategories.Delete(postCat);
        }
    }
}
