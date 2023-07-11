using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Plugin.Media;
using System.Threading.Tasks;

namespace Zwitscher.Services
{
    public class MediaConverter
    {
        public async static Task<IFormFile> SelectImage()
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return null;
            return ConvertToFormFile(file);
        }

        public static IFormFile ConvertToFormFile(MediaFile file)
        {
            // Erstellen Sie eine neue MemoryStream und kopieren Sie die Daten aus der MediaFile-Stream
            var memoryStream = new MemoryStream();
            file.GetStream().CopyTo(memoryStream);
            memoryStream.Position = 0;

            // Erstellen Sie ein neues FormFile-Objekt mit der MemoryStream und den erforderlichen Metadaten
            return new FormFile(memoryStream, 0, memoryStream.Length, file.Path, file.Path);
        }
    }
}
