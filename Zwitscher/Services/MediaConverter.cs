using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Plugin.Media;
using System.Threading.Tasks;
using Zwitscher.Models;

namespace Zwitscher.Services
{
    // Diese Klasse ist für die Konvertierung und Auswählen von Bildern und Videos zuständig
    public class MediaConverter
    {
        // Die Methode öffnet die Galerie und gibt ein Bild als IFormFile zurück
        public async static Task<IFormFile> SelectImage()
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return null;
            return ConvertToFormFile(file);
        }

        // Die Methode öffnet die Galerie und gibt ein Video als IFormFile zurück
        public async static Task<IFormFile> SelectVideo()
        {
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.PickVideoAsync();
            if (file == null)
                return null;
            return ConvertToFormFile(file);
        }

        // Diese Methode wandelt ein MediaFile in ein IFormFile um
        public static IFormFile ConvertToFormFile(MediaFile file)
        {
            // Erstellen Sie eine neue MemoryStream und kopieren Sie die Daten aus der MediaFile-Stream
            var memoryStream = new MemoryStream();
            file.GetStream().CopyTo(memoryStream);
            memoryStream.Position = 0;

            // Erstellen Sie ein neues FormFile-Objekt mit der MemoryStream und den erforderlichen Metadaten
            return new FormFile(memoryStream, 0, memoryStream.Length, file.Path, file.Path);
        }

        // Hier wird aus der mediaList eines Posts die Videopfade herausgefiltert
        public static List<string> GetVideoPath(List<string> mediaStrings)
        {
            if (AppConfig.disableVideo)
            {
                return new List<string>();
            }
            var videoPath = new List<string>();
            foreach (var mediaString in mediaStrings)
            {
                if (mediaString.Contains(".mp4"))
                {
                    videoPath.Add(mediaString);
                }
            }
            return videoPath;
        }

        // Hier wird aus der mediaList eines Posts die Bildpfade herausgefiltert
        public static List<string> GetImagePath(List<string> mediaStrings)
        {
            var imagePath = new List<string>();
            foreach (var mediaString in mediaStrings)
            {
                if (mediaString.Contains(".jpg") || mediaString.Contains(".png"))
                {
                    imagePath.Add(mediaString);
                }
            }
            return imagePath;
        }

        // Diese Methode konfiguriert den Pfad für die Profilbilder so, dass die App sie vom Server laden kann
        public static string ChangeProfilePath(string profilePath)
        {
            if (profilePath != "")
            {
                return AppConfig.ApiUrl + "/Media/" + profilePath;
            }
            else
            {
                return AppConfig.pbPlaceholderUrl;
            }
        }

        // Diese Methode konfiguriert den Pfad für die Medien so, dass die App sie vom Server laden kann
        public static List<string> ChangeMediaPath(Post post)
        {
            if (post.isRetweet)
            {
                post.mediaList = new List<string>();
            }

            for (int i = 0; i < post.mediaList.Count; i++)
            {
                post.mediaList[i] = AppConfig.ApiUrl + "/Media/" + post.mediaList[i];
            }
            return post.mediaList;
        }
    }
}
