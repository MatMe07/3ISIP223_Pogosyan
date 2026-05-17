using MaterialDesignThemes.Wpf;
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
            MyMessageQueue = new SnackbarMessageQueue();
        }

        public bool AddReview()
        {
            if (SelectRatingReview == 0)
                {
                ActionsClass.SnackBarEnqueue(
                    text: "Пожалуйста, оцените книгу (от 1 до 5)",
                    foregroundHEX: "#FFE74C3C",
                    iconKind: PackIconKind.Star,
                    MyMessageQueue: MyMessageQueue
                );
                return false;
            }
            if (string.IsNullOrWhiteSpace(EditorText))
            {
                ActionsClass.SnackBarEnqueue(
                    text: "Напишите текст отзыва",
                    foregroundHEX: "#FFE74C3C",
                    iconKind: PackIconKind.MessageText,
                    MyMessageQueue: MyMessageQueue
                );
                return false;

            }
            if (EditorText.Length < 10)
            {
                ActionsClass.SnackBarEnqueue(
                    text: $"Отзыв должен содержать минимум 10 символов (сейчас {EditorText.Length})",
                    foregroundHEX: "#FFE74C3C",
                    iconKind: PackIconKind.MessageAlert,
                    MyMessageQueue: MyMessageQueue
                );
                //MyMessageQueue.Enqueue()
                return false;

            }


            dataBase.AddReview(SelectRatingReview,EditorText);

            return true;

        }


        private double _selectRatingReview = 0;
        public double SelectRatingReview
        {
            get {  return _selectRatingReview; } 
            set {
                _selectRatingReview = value; 
                OnPropertyChanged(nameof(SelectRatingReview));
            }
        }

        public SnackbarMessageQueue MyMessageQueue { get; set; }


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
