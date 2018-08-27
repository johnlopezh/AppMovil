using System;
using System.IO;
using SGSApp.Droid.Services;
using SGSApp.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]

namespace SGSApp.Droid.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}