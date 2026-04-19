using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class StartedMainView : INotifyPropertyChanged
    {
        private WorkDataBase _dataService;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public User _SelectedMaster;
        public User SelectedMaster
        {
            get { return _SelectedMaster; }
            set
            {
                if (value == null) return;
                _SelectedMaster = value;
                _dataService.CurrentMaster = value;
            }
        }
        private ObservableCollection<User> _MasterSerives;
        public ObservableCollection<User> MasterSerives { get; set; }
        //{ get { return _MasterSerives; }
        //    set
        //    {
        //        _MasterSerives = value;
        //        OnPropertyChanged(nameof(MasterSerives));
        //    }
            
        //}
        public ObservableCollection<TypeService> TypeServes
        {
            get
            {
                return _dataService.TypeServes;
            }
        }
        public ObservableCollection<User> Users => _dataService.Users;

        private string _selectedMasterItemFilt;
        public string SelectedMasterItemFilt
        {
            get { return _selectedMasterItemFilt; }
            set
            {
                _selectedMasterItemFilt = value;
                OnPropertyChanged(nameof(SelectedMasterItemFilt));
                ApplyFilter();
            }
        }
        private string _selectedUslugItemFilt;
        public string SelectedUslugItemFilt
        {
            get { return _selectedUslugItemFilt; }
            set
            {
                _selectedUslugItemFilt = value;
                OnPropertyChanged(nameof(SelectedUslugItemFilt));
                ApplyFilter();
            }
        }
        private void ApplyFilter()
        {
            var filtered = _dataService.MasterServes.ToList();
            if (_selectedMasterItemFilt != null && _selectedMasterItemFilt != "Все производители")
            {
                filtered = filtered.Where(u => u.FIO == _selectedMasterItemFilt).ToList();
            }
            if (_selectedUslugItemFilt != null && _selectedUslugItemFilt != "Все типы")
            {
                filtered = filtered.Where(u => u.Uslugi.Contains(_selectedUslugItemFilt)).ToList();
            }

            MasterSerives = new ObservableCollection<User>(filtered);
        }
        public List<string> MastersStrings { get; set; }

        public List<string> TypeServesStrings { get; set; }

        public StartedMainView()
        {

            _dataService = WorkDataBase.Instance;
            MasterSerives =  _dataService.MasterServes;

            MastersStrings = MasterSerives.Select(x => x.FIO).Distinct().ToList();
            MastersStrings.Insert(0, "Все производители");

            TypeServesStrings = TypeServes.Select(x => x.Name).ToList();
            TypeServesStrings.Insert(0, "Все типы");

        }


    }
}
