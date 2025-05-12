using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmol_BookStore_Services.Interfaces
{
    public interface IImageService
    {
        bool ValidateImage(IFormFile imageFile, out string validationError);
        Task<string> SaveImageAsync(IFormFile imageFile, string baseFolderPath, string bookTitle);
    }
}
