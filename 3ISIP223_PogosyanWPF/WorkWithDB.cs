using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace _3ISIP223_PogosyanWPF
{
    public class WorkWithDB : INotifyPropertyChanged
    {
        public WorkWithDB() {
            _cpu = null;
            _cpuList = new ObservableCollection<cpu>( Core.Context.cpus.ToList());

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private cpu _cpu;
        public cpu Cpu
        {
            get { return _cpu; }
            set
            {
                _cpu = value;
                OnPropertyChanged(nameof(Cpu));
                
            }
        }

        public void SelectCPU(cpu c)
        {
            Cpu = c;

        }

        private ObservableCollection<cpu> _cpuList;
        public ObservableCollection<cpu> CpuList
        {
            get { return _cpuList; }
            set
            {
                _cpuList = value;
                OnPropertyChanged(nameof(CpuList));
            }
        }

    }

    public static class MarWorkWith
    {
        
        public static WorkWithDB withDB { get; } = new WorkWithDB();

    }

}
