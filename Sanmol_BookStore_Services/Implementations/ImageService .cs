using Microsoft.AspNetCore.Http;
using Sanmol_BookStore_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmol_BookStore_Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly string[] _allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        private const long _maxFileSize = 2 * 1024 * 1024; // 2MB

        public bool ValidateImage(IFormFile imageFile, out string validationError)
        {
            validationError = null;

            if (imageFile == null || imageFile.Length == 0)
            {
                validationError = "Please upload a cover image.";
                return false;
            }

            var extension = Path.GetExtension(imageFile.FileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
            {
                validationError = "Only .jpg, .jpeg, and .png files are allowed.";
                return false;
            }

            if (imageFile.Length > _maxFileSize)
            {
                validationError = "File size must be less than 2MB.";
                return false;
            }

            return true;
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, string baseFolderPath, string bookTitle)
        {
            // Sanitize book title for folder and filename
            var sanitizedBookTitle = string.Concat(bookTitle.Split(Path.GetInvalidFileNameChars()));

            var bookFolder = Path.Combine(baseFolderPath, sanitizedBookTitle);
            Directory.CreateDirectory(bookFolder); // Ensure folder exists

            var extension = Path.GetExtension(imageFile.FileName);

            // Use book title as filename with extension (e.g., Secret.jpg)
            var newFileName = $"{sanitizedBookTitle}{extension}";
            var fullPath = Path.Combine(bookFolder, newFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return the path to store in database
            return $"/images/books/{sanitizedBookTitle}/{newFileName}";
        }


    }

}
