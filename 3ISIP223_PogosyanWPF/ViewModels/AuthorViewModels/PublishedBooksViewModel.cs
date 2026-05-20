using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels.AuthorViewModels
{
    public class PublishedBooksViewModel : BaseViewModel
    {
        public PublishedBooksViewModel()
        {
            Books = new ObservableCollection<Book>(dataBase.GetAllAuthorBooks(dataBase.User.UserId));
        }

        private ObservableCollection<Book> _books;

        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { 
                _books = value;
                OnPropertyChanged(nameof(Books));
            }
        }
    }
}
