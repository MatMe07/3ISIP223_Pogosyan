using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace _3ISIP223_PogosyanWPF
{
    public partial class Book
    {
        //public double RatingFive => (Rating ?? 0) / 2.0;
        private static string _projectDirectory;
        public int? ReasonIdHesh {  get; set; } 

        
       public int CountReview
        {
            get
            {
                return Reviews.Count;
            }
        }
        public BitmapImage CoverImage
        {
            get
            {
                string fullPath = CoverFullPath;

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
                bitmap.UriSource = new Uri(fullPath);
                bitmap.DecodePixelWidth = 300; 
                bitmap.DecodePixelHeight = 400;
                bitmap.EndInit();
                //bitmap.Freeze();

                return bitmap;
            }
        }
        public string CoverFullPath
        {
            get
            {
                if (CoverPath == null) return @"pack://application:,,,/Images/Covers/empty_cover_booknet.jpg";
                string fullPath = GetFullPath(CoverPath);
                return fullPath;
            }
        }
        private string GetFullPath(string relativePath)
        {
            string path = relativePath.TrimStart('/', '\\').Replace('/', '\\');

            if (_projectDirectory == null)
            {
                string current = Environment.CurrentDirectory;
                for (int i = 0; i < 2; i++)
                {
                    current = Path.GetDirectoryName(current);
                    if (current == null) break;
                }
                _projectDirectory = current;
            }

            return Path.Combine(_projectDirectory, path);
        }
    }
}
