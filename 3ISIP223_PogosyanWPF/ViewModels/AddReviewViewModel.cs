using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class AddReviewViewModel : BaseViewModel
    {
        public AddReviewViewModel()
        {
            SelectRatingReview = 0;
        }

        public void AddReview()
        {
            dataBase.AddReview(SelectRatingReview,EditorText);
        }


        private double _selectRatingReview;
        public double SelectRatingReview
        {
            get {  return _selectRatingReview; } 
            set {
                _selectRatingReview = value; 
                OnPropertyChanged(nameof(SelectRatingReview));
            }
        }




        private string _editorText;
        public string EditorText
        {
            get => _editorText;
            set
            {
                _editorText = value;
                OnPropertyChanged(nameof(EditorText));
            }
        }

        public string UserName => dataBase.User.DisplayName;
    }
}
