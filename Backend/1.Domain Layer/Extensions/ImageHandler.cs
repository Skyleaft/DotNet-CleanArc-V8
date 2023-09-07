using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DomainLayer.Extensions
{
    public class ImageHandler
    {
        public async Task<string> UploadImageAsync(string contentPath, IFormFile imageFile)
        {
            string ext = Path.GetExtension(imageFile.FileName);
            string uniqueString = Guid.NewGuid().ToString();
            var path = Path.Combine(contentPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            string imageWebPath = "/Uploads/" + newFileName;
            using (FileStream stream = System.IO.File.Create(fileWithPath))
            {
                await imageFile.CopyToAsync(stream);
            }
            return imageWebPath;
        }

        public async Task<string> ReplaceImageAsync(string contentPath,string oldImage, IFormFile imageFile)
        {
            string ext = Path.GetExtension(imageFile.FileName);
            string uniqueString = Guid.NewGuid().ToString();
            var path = Path.Combine(contentPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            var oldfileWithPath = Path.Combine(path, oldImage);
            if(File.Exists(oldfileWithPath))
            {
                File.Delete(oldfileWithPath);
            }

            string imageWebPath = "/Uploads/" + newFileName;
            using (FileStream stream = System.IO.File.Create(fileWithPath))
            {
                await imageFile.CopyToAsync(stream);
            }
            return imageWebPath;
        }
    }
}
