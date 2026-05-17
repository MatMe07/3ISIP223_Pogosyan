using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF
{
    public partial class AuthorRequest
    {
        public Visibility VisibilityButtons
        {
            get
            {
                if (StatusesRequest.Name == "На рассмотрении") return Visibility.Visible;
                else return Visibility.Collapsed;
            }
            set { }
        }
    }
}
