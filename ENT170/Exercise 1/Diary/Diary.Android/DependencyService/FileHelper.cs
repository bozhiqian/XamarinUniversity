using System;
using System.IO;
using Diary.DependencyService;
using Diary.Droid.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Diary.Droid.DependencyService
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}