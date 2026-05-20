using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels.ProfileViewModels
{
    public class ProfileMyReviewsViewModel : BaseViewModel
    {
        public ProfileMyReviewsViewModel()
        {

        }

        private ObservableCollection<Review> _reviews;
        public ObservableCollection<Review> Reviews
        {
            get { return _reviews; }
            set { _reviews = value; 
                OnPropertyChanged(nameof(Review));
            }
        }

        public void UpdateReviews()
        {
            
        }
    }
}
