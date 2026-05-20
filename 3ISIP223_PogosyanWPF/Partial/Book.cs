using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF
{
    public partial class Book
    {
        //public double RatingFive => (Rating ?? 0) / 2.0;

        public int? ReasonIdHesh {  get; set; } 

        
       public int CountReview
        {
            get
            {
                return Reviews.Count;
            }
        }
        public string CoverFullPath
        {
            get
            {
                if (CoverPath == null) return "/Images/Covers/empty_cover_booknet.jpg";
                string fullPath = GetFullPath(CoverPath);
                return fullPath;
            }
        }
        private string GetFullPath(string relativePath)
        {
            string path = relativePath.TrimStart('/', '\\').Replace('/', '\\');

            string current = Environment.CurrentDirectory;
            for (int i = 0; i < 2; i++)
            {
                current = Path.GetDirectoryName(current);
                if (current == null) break;
            }

            return Path.Combine(current, path);
        }
    }
}
