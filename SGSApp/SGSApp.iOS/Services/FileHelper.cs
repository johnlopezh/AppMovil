using System;
using System.IO;
using SGSApp.iOS.Services;
using SGSApp.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace SGSApp.iOS.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = Path.Combine(docFolder, "..", "Databases");
            if (!Directory.Exists(libFolder)) Directory.CreateDirectory(libFolder);
            return Path.Combine(libFolder, fileName);
        }
    }
}