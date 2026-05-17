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
            Complaints = dataBase.Complaints;
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

    }
}
