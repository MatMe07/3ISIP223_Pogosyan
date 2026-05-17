using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels.AdminViewModels
{
    public class AuthorRequestsViewModel:BaseViewModel
    {
        public AuthorRequestsViewModel()
        {
            MyMessageQueue = new SnackbarMessageQueue();

        }
        private ObservableCollection<AuthorRequest> _authorRequest;
        public ObservableCollection<AuthorRequest> AuthorRequest
        {
            get { return _authorRequest; }
            set
            {
                _authorRequest = value;
                OnPropertyChanged(nameof(AuthorRequest));
            }
        }
        public void LoadData()
        {
            AuthorRequest = new ObservableCollection<AuthorRequest>(dataBase.AuthorRequests.OrderByDescending(com => com.StatusesRequest.Name == "На рассмотрении")
                .ThenByDescending(com => com.CreatedAt));
        }

        public SnackbarMessageQueue MyMessageQueue { get; set; }


        public void AcceptClick(AuthorRequest request)
        {
            ActionsClass.SnackBarEnqueue(
                text: $"Заявка #{request.RequestId} от пользователя \"{request.User.DisplayName}\" на получение роли автора одобрена. Пользователь теперь автор.",
                buttonText: "Отменить",
                foregroundHEX: "#FF04BE5A",
                iconKind: PackIconKind.AccountCheck,
                onButtonClick: () => CancelAcceptRejectAuthorRequest(request),
                MyMessageQueue: MyMessageQueue,
                main: true
            );
            //MainWindow.GetInstance().MySnackbar.IsActiveChanged += (s, e) => MySnackbar_IsActiveChanged(s, e, complaint);


            dataBase.AcceptAuthorRequest(request);
            LoadData();
        }


        public void CancelAcceptRejectAuthorRequest(AuthorRequest request)
        {
            dataBase.CancelAcceptRejectAuthorRequest(request);
            LoadData();

            ActionsClass.SnackBarEnqueue(
                text: "Действие отменено. Заявка возвращена на рассмотрение.",
                foregroundHEX: "#FFFF9800",
                iconKind: PackIconKind.Undo,
                MyMessageQueue: MyMessageQueue,
                main: true
            );
        }
        public void RejectClick(AuthorRequest request)
        {
            dataBase.RejectAuthorRequest(request);
            LoadData();

            ActionsClass.SnackBarEnqueue(
                text: $"Заявка #{request.RequestId} от пользователя \"{request.User.DisplayName}\" на получение роли автора отклонена.",
                buttonText: "Отменить",
                foregroundHEX: "#FFE74C3C",
                iconKind: PackIconKind.CloseCircle,
                onButtonClick: () => CancelAcceptRejectAuthorRequest(request),
                MyMessageQueue: MyMessageQueue,
                main: true
            );

        }


    }
}
