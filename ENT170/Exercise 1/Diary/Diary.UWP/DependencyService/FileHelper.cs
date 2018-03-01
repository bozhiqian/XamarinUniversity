using System.IO;
using Windows.Storage;
using Diary.DependencyService;
using Diary.UWP.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Diary.UWP.DependencyService
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}