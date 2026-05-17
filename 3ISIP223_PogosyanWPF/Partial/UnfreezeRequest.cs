using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF
{
    public partial class UnfreezeRequest
    {
        public string GetComplType
        {
            get
            {
                string CompT = "";

                switch (TargetType.Name)
                {
                    case "Author":
                        {
                            CompT = "Автор";
                            break;
                        }
                    case "Book":
                        {
                            CompT = "Книга";
                            break;
                        }
                }
                return CompT;
            }
            set { }
        }

        public Visibility VisibilityButtons
        {
            get
            {
                return StatusesRequest.Name == "На рассмотрении" ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility GetIsBook
        {
            get
            {
                return TargetType.Name == "Book" ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public string GetTargetName()
        {
            if (TargetType.Name == "Book")
            {
                return Book.Title;
            }
            else
            {
                return User.DisplayName;
            }
        }
    }
}
