using Microsoft.EntityFrameworkCore;
using Sanmol_BookStore_Models;
using Sanmol_BookStore_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmol_BookStore_Services.Implementations
{
    public class CategoryService:ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly BookStoreDbContext _context;

        public CategoryService(IRepository<Category> categoryRepository, BookStoreDbContext context)
        {
            _categoryRepository = categoryRepository;
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                         .Include(c => c.Books)
                         .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = await GetCategoryByIdAsync(category.Id);

           
            if (existingCategory?.Books != null && existingCategory.Books.Any())
            {
                throw new InvalidOperationException("Category cannot be edited because it has associated books.");
            }

            existingCategory.Name = category.Name; 
            _categoryRepository.Update(existingCategory);
            await _categoryRepository.SaveAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);

            if (category != null)
            {
                // Check if any books are linked to the category
                if (category.Books != null && category.Books.Any())
                {
                    throw new InvalidOperationException("Category cannot be deleted because it has associated books.");
                }

                _categoryRepository.Delete(category);
                await _categoryRepository.SaveAsync();
            }
        }
    }
}
