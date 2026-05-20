using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class UnfreezeRequestsViewModel : BaseViewModel
    {
        public UnfreezeRequestsViewModel()
        {
            MyMessageQueue = new SnackbarMessageQueue();

        }
        public string TypeRequest = "";
        public int LengthUnfreezeText => TextUnfreeze.Length;
        public SnackbarMessageQueue MyMessageQueue { get; set; }


        private string _textUnfreeze = "";
        public string TextUnfreeze 
        {
            get { return _textUnfreeze; }
            set
            {
                _textUnfreeze = value;
                OnPropertyChanged(nameof(TextUnfreeze));
                OnPropertyChanged(nameof(LengthUnfreezeText));
            }
        } 

        public bool SendRequest(int? bookId)
        {
            if (_textUnfreeze.Length < 20)
            {
                ActionsClass.SnackBarEnqueue(
                    text: $"Количество символов < 20!",
                    foregroundHEX: "#F25022",
                    iconKind: PackIconKind.Cancel,
                    MyMessageQueue: MyMessageQueue
                );
                return false;
            }
            if (TypeRequest == "Book")
                dataBase.SendRequest(TextUnfreeze, TypeRequest, bookId);
            else
            {
                dataBase.SendRequest(TextUnfreeze, TypeRequest);

            }


            return true; 
        }
    }
}
