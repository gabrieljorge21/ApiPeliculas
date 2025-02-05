using ApiPelículas.Models;

namespace ApiPelículas.Repository.IRepository
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAll();
        Category GetById(int id);
        bool ExistsById(int id);
        bool ExistsByName(string name);
        bool Create(Category category);
        bool Update(Category category);
        bool Delete(Category category);
        bool Save();
        
    }
}
