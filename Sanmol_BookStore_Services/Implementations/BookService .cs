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
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly BookStoreDbContext _context;

        public BookService(IRepository<Book> bookRepository, BookStoreDbContext context)
        {
            _bookRepository = bookRepository;
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            try
            {
                return await _context.Books
                    .Include(b => b.Category)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("An error occurred while retrieving all books.", ex);
            }
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            try
            {
                return await _context.Books
                    .Include(b => b.Category)
                    .FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving the book with ID {id}.", ex);
            }
        }

        public async Task AddBookAsync(Book book)
        {
            try
            {
                await _bookRepository.AddAsync(book);
                await _bookRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the book.", ex);
            }
        }

        public async Task UpdateBookAsync(Book book)
        {
            try
            {
                _bookRepository.Update(book);
                await _bookRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the book.", ex);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book != null)
                {
                    _bookRepository.Delete(book);
                    await _bookRepository.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting the book with ID {id}.", ex);
            }
        }


    }
}
