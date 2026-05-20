using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF.ViewModels.ProfileViewModels
{
    public class ProfileMyReviewsViewModel : BaseViewModel
    {
        public ProfileMyReviewsViewModel()
        {
            Reviews = new ObservableCollection<Review>(dataBase._AllReviews.Where(r => r.UserId == dataBase.User.UserId));

        }

        private ObservableCollection<Review> _reviews;
        public ObservableCollection<Review> Reviews
        {
            get { return _reviews; }
            set { _reviews = value; 
                OnPropertyChanged(nameof(Review));
            }
        }

        public Visibility IsReviews => Reviews.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        public Visibility IsNotReviews => Reviews.Count == 0 ? Visibility.Visible : Visibility.Collapsed;

        public void UpdateReviews()
        {
            Reviews = new ObservableCollection<Review>( dataBase._AllReviews.Where(r => r.UserId == dataBase.User.UserId));
        }
    }
}
