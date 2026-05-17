using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels.AdminViewModels
{
    public class UnfreezeRequestsViewModel : BaseViewModel
    {
        public UnfreezeRequestsViewModel()
        {
            //UnfreezeRequests = dataBase.GetUnfreezeRequests;
            //LoadData();
            MyMessageQueue = new SnackbarMessageQueue();

        }

        public void LoadData()
        {
            UnfreezeRequests = new ObservableCollection<UnfreezeRequest>( dataBase.unfreezeRequests.OrderByDescending(a=>a.StatusesRequest.Name == "На рассмотрении").
                ThenByDescending(uf=>uf.CreatedAt));
        }

        private ObservableCollection<UnfreezeRequest> _unfreezeRequests;

        public ObservableCollection<UnfreezeRequest> UnfreezeRequests
        {
            get
            {
                return _unfreezeRequests;
            }
            set
            {
                _unfreezeRequests = value;
                OnPropertyChanged(nameof(UnfreezeRequests));
            }
        }


        public SnackbarMessageQueue MyMessageQueue { get; set; }

        public void AcceptClick(UnfreezeRequest request)
        {
            ActionsClass.SnackBarEnqueue(
                text: $"Заявка #{request.RequestId} на разморозку {request.GetComplType}  \"{request.GetTargetName()}\" принята. Объект разморожен.",
                buttonText: "Отменить",
                foregroundHEX: "#FF04BE5A",
                iconKind: PackIconKind.CheckCircle,
                onButtonClick: () => CancelAcceptUnfreezeRequest(request),
                MyMessageQueue: MyMessageQueue,
                main: true
            );
            //MainWindow.GetInstance().MySnackbar.IsActiveChanged += (s, e) => MySnackbar_IsActiveChanged(s, e, complaint);


            dataBase.AcceptUnfreezeRequest(request);
            LoadData();
        }


        public void CancelAcceptUnfreezeRequest(UnfreezeRequest complaint)
        {
            Console.WriteLine(complaint.GetComplType);
            dataBase.CancelAcceptRejectUnfreezeRequest(complaint);

            LoadData();
            ActionsClass.SnackBarEnqueue(
                text: "Действие отменено. Заявка возвращена на рассмотрение.",
                foregroundHEX: "#FFFF9800",
                iconKind: PackIconKind.Undo,
                MyMessageQueue: MyMessageQueue,
                main: true
            );

        }
        public void RejectClick(UnfreezeRequest request)
        {
            ActionsClass.SnackBarEnqueue(
                text: $"Заявка #{request.RequestId} на разморозку {request.GetComplType} \"{request.GetTargetName()}\" отклонена.",
                buttonText: "Отменить",
                foregroundHEX: "#FFE74C3C",
                iconKind: PackIconKind.CheckCircle,
                onButtonClick: () => CancelAcceptUnfreezeRequest(request),
                MyMessageQueue: MyMessageQueue,
                main: true
                );
            dataBase.RejectUnfreezeRequest(request);

            LoadData();

        }

    }
}
