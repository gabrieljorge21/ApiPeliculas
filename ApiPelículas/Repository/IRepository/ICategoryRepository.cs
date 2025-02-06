using ApiPelículas.Models;

namespace ApiPelículas.Repository.IRepository
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategories();
        Category GetCategoryById(int id);
        bool ExistsById(int id);
        bool ExistsByName(string name);
        bool Create(Category category);
        bool Update(Category category);
        bool Delete(Category category);
        bool Save();
        
    }
}
