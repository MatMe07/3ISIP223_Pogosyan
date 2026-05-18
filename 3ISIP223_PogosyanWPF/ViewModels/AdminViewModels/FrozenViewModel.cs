using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels.AdminViewModels
{
    public class FrozenViewModel : BaseViewModel
    {
        public FrozenViewModel()
        {

        }

        public void LoadData()
        {
            FrozenBooks = new ObservableCollection<Book>(dataBase.Books.Where(b=>b.IsFrozen));
            FrozenUsers = new ObservableCollection<User>(dataBase.Users.Where(u=>u.IsFrozen));
            Reviews = new ObservableCollection<Review>(dataBase._AllReviews.Where(r=>r.IsFrozen));
        }

        private ObservableCollection<Book> _frozenBooks;
        public ObservableCollection<Book> FrozenBooks
        {
            get { return _frozenBooks; }
            set
            {
                _frozenBooks = value;
                OnPropertyChanged(nameof(FrozenBooks));
            }
        }

        private ObservableCollection<User> _frozenUsers;
        public ObservableCollection<User> FrozenUsers
        {
            get { return _frozenUsers; }
            set
            {
                _frozenUsers = value;
                OnPropertyChanged(nameof(FrozenUsers));
            }
        }

        private ObservableCollection<Review> _reviews;
        public ObservableCollection<Review> Reviews
        {
            get
            {
                return _reviews;
            }
            set
            {
                _reviews = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }

    }
}
