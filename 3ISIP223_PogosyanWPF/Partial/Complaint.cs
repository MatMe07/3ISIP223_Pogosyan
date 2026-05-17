using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    public partial class Complaint
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
                    case "Review":
                        {
                            CompT = "Отзыв к книге";
                            break;
                        }
                }

                return CompT;
            }
            set {}
        }


        public string GetComplTypeName
        {
            get
            {
                string CompT = "";

                switch (TargetType.Name)
                {
                    case "Author":
                        {
                            CompT = User1.DisplayName;
                            break;
                        }
                    case "Book":
                        {
                            CompT = Book.Title;
                            break;
                        }
                    case "Review":
                        {

                            CompT = Review.Book.Title;
                            break;
                        }
                }

                return CompT;
            }
            set {}
        }
        public int GetComplTypeId
        {
            get
            {
                int CompT = 0;

                switch (TargetType.Name)
                {
                    case "Author":
                        {
                            CompT = User1.UserId;
                            break;
                        }
                    case "Book":
                        {
                            CompT = Book.BookId;
                            break;
                        }
                    case "Review":
                        {

                            CompT = Review.ReviewId;
                            break;
                        }
                }
                return CompT;
            }
            set {}
        }
    }
}
