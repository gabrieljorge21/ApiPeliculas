using ApiPelículas.Data;
using ApiPelículas.Models;
using ApiPelículas.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiPelículas.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }


        public bool Create(Category category)
        {
            category.CreationDate = DateTime.Now;
            _context.Categories.Add(category);
            return Save();
        }

        public bool Delete(Category category)
        {
            _context.Categories.Remove(category);
            return Save();
        }

        public bool ExistsById(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool ExistsByName(string name)
        {
            return _context.Categories.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public ICollection<Category> GetAll()
        {
            return _context.Categories.OrderBy(c => c.Name).ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public bool Save()  
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool Update(Category category)
        {
            category.CreationDate = DateTime.Now;
            _context.Categories.Update(category);
            return Save();
        }
    }
}
