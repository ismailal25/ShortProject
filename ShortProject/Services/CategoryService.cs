using ShortProject.Models;

namespace ShortProject.Services
{
    public class CategoryService
    {
        public void CreateCategory(Category category)
        {
            
            Array.Resize(ref DB.Categories, DB.Categories.Length + 1);
            DB.Categories[DB.Categories.Length - 1] = category;
        }
    }
}
