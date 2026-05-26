using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class FreezeRequestViewModel : BaseViewModel
    {

        public FreezeRequestViewModel()
        {
            ReasonLst = dataBase.GetReasons;
            //ReasonLst = new ObservableCollection<string>( dataBase.GetReasons.Select(a=>a.Name));
            MyMessageQueue = new SnackbarMessageQueue();
        }

        private Reason _selReason;
        public Reason SelReason
        {
            get { return _selReason; }
            set { 
                _selReason = value;
                OnPropertyChanged(nameof(SelReason));   
            }
        }

        private ObservableCollection<Reason> _reasonLst;
        public ObservableCollection<Reason> ReasonLst
        {
            get { return _reasonLst; }

            set { 
                _reasonLst = value; 
                OnPropertyChanged(nameof(ReasonLst));
            }
        }

        public void ChangeTitle(string title, bool isAdmin = false)
        {
            if (!isAdmin)
                AboutTitle = $"Подать жалобу на {title}";
            else
            {
                AboutTitle = $"Заморозить {title}";
            }
        }
        private string _aboutTitle  = "";
        public string AboutTitle
        {
            get
            {
                return _aboutTitle;
            }
            set
            {
                _aboutTitle = value;
                OnPropertyChanged(nameof(AboutTitle));
            }
        }
        public SnackbarMessageQueue MyMessageQueue { get; set; }

        public bool SaveRequest(Object items, bool IsAdmin)
        {
            if (SelReason == null)
            {
                ActionsClass.SnackBarEnqueue(
                    text: "Пожалуйста, укажите причину жалобы",
                    foregroundHEX: "#FFE74C3C",
                    iconKind: PackIconKind.Alert,
                    MyMessageQueue: MyMessageQueue
                );
                return false;
            }
            if (!IsAdmin)
            {
                dataBase.SaveFreezeRequest(items, SelReason);

            }
            else
            {
                dataBase.FreezeUserReviewAdmin(items, SelReason);
            }
            Console.WriteLine(SelReason.Name);
            return true;
        }
    }
}
