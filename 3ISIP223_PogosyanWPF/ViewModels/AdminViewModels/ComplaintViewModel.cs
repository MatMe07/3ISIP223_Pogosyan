using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels.AdminViewModels
{
    public class ComplaintViewModel : BaseViewModel
    {
        public ComplaintViewModel()
        {
            Complaints = new ObservableCollection<Complaint>( dataBase.Complaints.OrderByDescending(com=>com.StatusesRequest.Name == "На рассмотрении"));
            MyMessageQueue = new SnackbarMessageQueue();
        }

        private ObservableCollection<Complaint> _complaints;
        public ObservableCollection<Complaint> Complaints
        {
            get {  return _complaints; }
            set { 
                _complaints = value; 
                OnPropertyChanged(nameof(Complaints));
            }
        }
        public void UpdComplaints()
        {
            Complaints = new ObservableCollection<Complaint>(dataBase.Complaints.OrderByDescending(com => com.StatusesRequest.Name == "На рассмотрении"));

        }

        public SnackbarMessageQueue MyMessageQueue { get; set; }    

        public void AcceptClick(Complaint complaint)
        {
            ActionsClass.SnackBarEnqueue(
                text: $"Жалоба #{complaint.ComplaintId} на {complaint.GetComplType} \"{complaint.GetComplTypeName}\" принята. Объект заморожен.",
                buttonText: "Отменить",
                foregroundHEX: "#FF04BE5A",
                iconKind: PackIconKind.CheckCircle,
                onButtonClick: () => CancelAcceptRejectComplaint(complaint),
                MyMessageQueue: MyMessageQueue,
                main: true
                );
            //MainWindow.GetInstance().MySnackbar.IsActiveChanged += (s, e) => MySnackbar_IsActiveChanged(s, e, complaint);


            dataBase.AcceptComplain( complaint );
            UpdComplaints();
        }


        public void CancelAcceptRejectComplaint(Complaint complaint)
        {
            Console.WriteLine(complaint.GetComplType);
            dataBase.CancelAcceptRejectComplaint(complaint);

            UpdComplaints();
            ActionsClass.SnackBarEnqueue(
                text: "Действие отменено. Жалоба возвращена на рассмотрение.",
                foregroundHEX: "#FFFF9800",
                iconKind: PackIconKind.Undo,
                MyMessageQueue: MyMessageQueue,
                main: true
            );

        }
        public void RejectClick(Complaint complaint)
        {
            ActionsClass.SnackBarEnqueue(
                text: $"Жалоба #{complaint.ComplaintId} на {complaint.GetComplType} \"{complaint.GetComplTypeName}\" отклонена. Нарушений не найдено.",
                buttonText: "Отменить",
                foregroundHEX: "#FFE74C3C",
                iconKind: PackIconKind.CheckCircle,
                onButtonClick: () => CancelAcceptRejectComplaint(complaint),
                MyMessageQueue: MyMessageQueue,
                main: true
                );
            dataBase.RejectComplain(complaint);

            UpdComplaints();

        }

    }
}
