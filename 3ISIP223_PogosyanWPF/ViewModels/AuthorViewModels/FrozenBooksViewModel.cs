using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF.ViewModels.AuthorViewModels
{
    public class FrozenBooksViewModel : BaseViewModel
    {
        public FrozenBooksViewModel()
        {
            Books = new ObservableCollection<Book>(dataBase.GetAllAuthorBooks(dataBase.User.UserId).Where(b=>b.IsFrozen));
        }
        public int CountBooksFreeze
        {
            get { return Books.Count; }
            set
            {
                OnPropertyChanged(nameof(IsFreezeBooks));
                OnPropertyChanged(nameof(CountBooksFreeze));
            }
        }
        public bool IsFreezeBooks
        {
            get
            {
                return CountBooksFreeze == 0;
            }
            set
            {
                OnPropertyChanged(nameof(IsFreezeBooks));
                OnPropertyChanged(nameof(CountBooksFreeze));
            }
        }

        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books {
            get
            {
                return _books;
            }
            set
            {
                _books = value;
                OnPropertyChanged(nameof(IsFreezeBooks));
                OnPropertyChanged(nameof(CountBooksFreeze));
            }
        }   
    }
}
