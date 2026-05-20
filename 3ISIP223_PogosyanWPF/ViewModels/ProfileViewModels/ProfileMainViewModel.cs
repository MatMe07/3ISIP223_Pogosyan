using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF.ViewModels.ProfileViewModels
{
    public class ProfileMainViewModel : BaseViewModel
    {
        public ProfileMainViewModel()
        {

            User = dataBase.User;
        }
        public void UpdateUser()
        {
            User = dataBase.User;
        }

        public void SendAuthorRequest()
        {
            var existingRequest = dataBase.AuthorRequests.LastOrDefault(r => r.UserId == User.UserId && r.StatusId == 1);
            if (existingRequest != null)
            {
                ActionsClass.SnackBarEnqueue(
                    text: $"Запрос на роль автора уже отправлен {existingRequest.CreatedAt:dd.MM.yyyy HH:mm}.\nОжидайте ответа",
                            foregroundHEX: "#FFFFA500",
                    iconKind: PackIconKind.Information,
                    MyMessageQueue: new SnackbarMessageQueue(),
                    true
                );
            }
            else
            {
                dataBase.GetAuthorReq();
                ActionsClass.SnackBarEnqueue(
                    text: "Заявка на роль автора успешно отправлена!",
                    foregroundHEX: "#FF04BE5A",
                    iconKind: PackIconKind.CheckCircle,
                    MyMessageQueue: new SnackbarMessageQueue(), true
                );
            }
        }

        //public Visibility IsUserReader => User.IsUserVisibility

        private User _user;
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
    }
}
