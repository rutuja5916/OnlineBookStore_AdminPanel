using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sanmol_BookStore_Models;
using Sanmol_BookStore_Services.Implementations;
using Sanmol_BookStore_Services.Interfaces;

namespace Sanmol_BookStore.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IImageService _imageService;

        public BookController(IBookService bookService, ICategoryService categoryService, IImageService imageService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();

                if (!string.IsNullOrWhiteSpace(searchQuery))
                {
                    books = books.Where(b =>
                        b.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                        b.Author.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                        b.ISBN.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }

                ViewData["SearchQuery"] = searchQuery;

                return View(books);
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError("", "An error occurred while retrieving books.");
                return View(new List<Book>()); 
            }
        }


        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null) return NotFound();
                return View(book);
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError("", "An error occurred while retrieving the book details.");
                return View();
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View();
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError("", "An error occurred while loading the category list.");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile CoverImage, string ExistingCoverImagePath)
        {
            try
            {
                // Remove model validation for image fields not part of Book model
                ModelState.Remove("CoverImage");
                ModelState.Remove("ExistingCoverImagePath");

                // Case 1: No image provided at all
                if (CoverImage == null && string.IsNullOrEmpty(ExistingCoverImagePath))
                {
                    ModelState.AddModelError("CoverImage", "Please upload a cover image.");
                }

                // Case 2: New image uploaded
                if (CoverImage != null)
                {
                    if (!_imageService.ValidateImage(CoverImage, out string validationError))
                    {
                        ModelState.AddModelError("CoverImage", validationError);
                    }
                    else
                    {
                        var savedImagePath = await _imageService.SaveImageAsync(
                            CoverImage,
                            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books"),
                            book.Title
                        );
                        book.CoverImagePath = savedImagePath;
                    }
                }

                // Case 3: Use existing image if no new image is uploaded
                if (CoverImage == null && !string.IsNullOrEmpty(ExistingCoverImagePath))
                {
                    book.CoverImagePath = ExistingCoverImagePath;
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                    return View(book);
                }

                await _bookService.AddBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the book.");
                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(book);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null) return NotFound();

                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(book);
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", "An error occurred while retrieving the book for editing.");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book, IFormFile CoverImage, string ExistingCoverImagePath)
        {
            try
            {
                // Remove automatic model state validation for these fields
                ModelState.Remove("CoverImage");
                ModelState.Remove("ExistingCoverImagePath");

                // Case 1: No image provided and no existing image
                if (CoverImage == null && string.IsNullOrEmpty(ExistingCoverImagePath))
                {
                    ModelState.AddModelError("CoverImage", "Please upload a cover image.");
                }

                // Case 2: New image uploaded
                if (CoverImage != null)
                {
                    if (!_imageService.ValidateImage(CoverImage, out string validationError))
                    {
                        ModelState.AddModelError("CoverImage", validationError);
                    }
                    else
                    {
                        var savedImagePath = await _imageService.SaveImageAsync(
                            CoverImage,
                            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books"),
                            book.Title
                        );
                        book.CoverImagePath = savedImagePath;
                    }
                }

                // Case 3: Keep existing image if no new image uploaded
                if (CoverImage == null && !string.IsNullOrEmpty(ExistingCoverImagePath))
                {
                    book.CoverImagePath = ExistingCoverImagePath;
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                    return View(book);
                }

                await _bookService.UpdateBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while editing the book.");
                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(book);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null) return NotFound();
                return View(book);
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError("", "An error occurred while retrieving the book for deletion.");
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", "An error occurred while deleting the book.");
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Clone(int id)
        {
            try
            {
                
                var existingBook = await _bookService.GetBookByIdAsync(id);
                if (existingBook == null)
                {
                    return NotFound();
                }

                var clonedBook = new Book
                {
                    Title = existingBook.Title,
                    Author = existingBook.Author,
                    ISBN = existingBook.ISBN,
                    Price = existingBook.Price,
                    CategoryId = existingBook.CategoryId,
                    CoverImagePath = existingBook.CoverImagePath,
                    
                };

                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View("Create", clonedBook);
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", "An error occurred while cloning the book.");
                return View("Create");
            }
        }
    }
}
