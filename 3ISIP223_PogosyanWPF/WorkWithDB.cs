using _3ISIP223_PogosyanWPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace _3ISIP223_PogosyanWPF
{
    public class WorkWithDB : INotifyPropertyChanged
    {
        public WorkWithDB() {
            _cpu = null;

            _cpuAllList = new List<cpu>( Core.Context.cpus.ToList());
            _gpuAllList = new List<gpu>(Core.Context.gpus.ToList());
            _motherboardAllList = new List<motherboard>(Core.Context.motherboards.ToList());
            _powersupplyAllList = new List<powersupply>(Core.Context.powersupplies.ToList());
            _processorcoolerAllList = new List<processorcooler>(Core.Context.processorcoolers.ToList());
            _ramAllList = new List<ram>(Core.Context.rams.ToList());
            _caseAllList = new List<@case>(Core.Context.cases.ToList());
            _storageAllList = new List<storagedevice>(Core.Context.storagedevices.ToList());

            _cpuList = new ObservableCollection<cpu>(_cpuAllList);
            _gpuList = new ObservableCollection<gpu>(_gpuAllList);
            _motherboardList = new ObservableCollection<motherboard>(_motherboardAllList);
            _powersupplyList = new ObservableCollection<powersupply>(_powersupplyAllList);
            _processorcoolerList = new ObservableCollection<processorcooler>(_processorcoolerAllList);
            _ramList = new ObservableCollection<ram>(_ramAllList);
            _caseList = new ObservableCollection<@case>(_caseAllList);
            _storageList = new ObservableCollection<storagedevice>(_storageAllList);


            manufacturers = Core.Context.manufacturers.Select(s=>s.name).ToList();
            manufacturers.Insert(0, "Все");
            //manufacturers[0].name
            CountConfig = 0;
            OnPropertyChanged(nameof(CountConfig));

        }
        public int CountConfig { get; set; }


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
                CountConfig += value == null ? -1 : (_cpu != null ? 0 : 1);
                OnPropertyChanged(nameof(CountConfig));
                _cpu = value;
                OnPropertyChanged(nameof(Cpu));
                
            }
        }
        private motherboard _motherboard;
        public motherboard Motherboard
        {
            get { return _motherboard; }
            set
            {
                CountConfig += value == null ? -1 : (_motherboard != null ? 0 : 1);
                OnPropertyChanged(nameof(CountConfig));
                _motherboard = value;
                OnPropertyChanged(nameof(Motherboard));

            }
        }
        private @case _case;
        public @case Case
        {
            get { return _case; }
            set
            {
                CountConfig += value == null ? -1 : (_case != null ? 0 : 1);
                OnPropertyChanged(nameof(CountConfig));
                _case = value;
                OnPropertyChanged(nameof(Case));
            }
        }
        private gpu _gpu;
        public gpu GPU
        {
            get { return _gpu; }
            set
            {
                CountConfig += value == null ? -1 : (_gpu != null ? 0 : 1);
                OnPropertyChanged(nameof(CountConfig));
                _gpu = value;
                OnPropertyChanged(nameof(GPU));
            }
        }
        private ram _ram;
        public ram RAM
        {
            get { return _ram; }
            set
            {
                CountConfig += value == null ? -1 : (_ram != null ? 0 : 1);
                OnPropertyChanged(nameof(CountConfig));
                _ram = value;
                OnPropertyChanged(nameof(RAM));
            }
        }
        private powersupply _powersupply;
        public powersupply Powersupply
        {
            get { return _powersupply; }
            set
            {
                CountConfig += value == null ? -1 : (_powersupply != null ? 0 : 1);
                OnPropertyChanged(nameof(CountConfig));
                _powersupply = value;
                OnPropertyChanged(nameof(Powersupply));
            }
        }
        private processorcooler _processorcooler;
        public processorcooler Processorcooler
        {
            get { return _processorcooler; }
            set
            {
                CountConfig += value == null ? -1 : (_processorcooler != null ? 0 : 1);
                OnPropertyChanged(nameof(CountConfig));
                _processorcooler = value;
                OnPropertyChanged(nameof(Processorcooler));
            }
        }
        private storagedevice _storagedevice;
        public storagedevice Storage
        {
            get { return _storagedevice; }
            set
            {
                CountConfig += value == null ? -1 : (_storagedevice != null ? 0 : 1);
                OnPropertyChanged(nameof(CountConfig));
                _storagedevice = value;
                OnPropertyChanged(nameof(Storage));
            }
        }

        public void SelectComponent(Object obj, ComponentType type)
        {
            switch (type)
            {
                case ComponentType.CPU:
                    {
                        Cpu = obj as cpu;
                        break;
                    }
                case ComponentType.GPU:
                    {
                        GPU = obj as gpu;
                        break;
                    }
                case ComponentType.Motherboard:
                    {
                        Motherboard = obj as motherboard;
                        break;
                    }
                case ComponentType.ProcessorCooler:
                    {
                        Processorcooler = obj as processorcooler;
                        break;
                    }
                case ComponentType.Case:
                    {
                        Case = obj as @case;
                        break;
                    }
                case ComponentType.PowerSupply:
                    {
                        Powersupply = obj as powersupply;
                        break;
                    }
                case ComponentType.RAM:
                    {
                        RAM = obj as ram;
                        break;
                    }
                case ComponentType.Storage:
                    {
                        Storage = obj as storagedevice;
                        break;
                    }
            }
        }
        public void SearchFilterComponent(ComponentType type, string search, string filter)
        {
            


            //var filterList = null;
            switch (type)
            {
                case ComponentType.CPU:
                    {
                        var filterList = _cpuAllList;
                        filterList = filterList.Where(f => f.basepart.name.Contains(search)).ToList();
                        if (filter != "Все") filterList = filterList.Where(f => f.basepart.manufacturer.name == filter).ToList();
                        CpuList.Clear();
                        foreach (var item in filterList)
                        {
                            CpuList.Add(item);
                        }
                        break;
                    }
                case ComponentType.GPU:
                    {
                        //GPU = obj as gpu;
                        break;
                    }
                case ComponentType.Motherboard:
                    {
                        //Motherboard = obj as motherboard;
                        break;
                    }
                case ComponentType.ProcessorCooler:
                    {
                        //Processorcooler = obj as processorcooler;
                        break;
                    }
                case ComponentType.Case:
                    {
                        //Case = obj as @case;
                        break;
                    }
                case ComponentType.PowerSupply:
                    {
                        //Powersupply = obj as powersupply;
                        break;
                    }
                case ComponentType.RAM:
                    {
                        //RAM = obj as ram;
                        break;
                    }
                case ComponentType.Storage:
                    {
                        //Storage = obj as storagedevice;
                        break;
                    }
            }
        }


        public void DeleteComponent(ComponentType type)
        {
            switch(type)
            {
                case ComponentType.CPU:
                    {
                        Cpu = null;
                        break;
                    }
                case ComponentType.RAM:
                    {
                        RAM = null;
                        break;
                    }
                case ComponentType.Motherboard:
                    {
                        Motherboard = null;
                        break;
                    }
                case ComponentType.Case:
                    {
                        Case = null;
                        break;
                    }
                case ComponentType.GPU:
                    {
                        GPU = null;
                        break;
                    }
                case ComponentType.ProcessorCooler:
                    {
                        Processorcooler = null;
                        break;
                    }
                case ComponentType.PowerSupply:
                    {
                        Powersupply = null;
                        break;
                    }
                case ComponentType.Storage:
                    {
                        Storage = null;
                        break;
                    }
            
            }
        }




        private ObservableCollection<cpu> _cpuList;
        private List<cpu> _cpuAllList;
        public ObservableCollection<cpu> CpuList
        {
            get { return _cpuList; }
            set
            {
                _cpuList = value;
                OnPropertyChanged(nameof(CpuList));
            }
        }

        private ObservableCollection<motherboard> _motherboardList;
        private List<motherboard> _motherboardAllList;
        public ObservableCollection<motherboard> MotherboardList
        {
            get { return _motherboardList; }
            set
            {
                _motherboardList = value;
                OnPropertyChanged(nameof(MotherboardList));
            }
        }

        private ObservableCollection<@case> _caseList;
        private List<@case> _caseAllList;
        public ObservableCollection<@case> CaseList
        {
            get { return _caseList; }
            set
            {
                _caseList = value;
                OnPropertyChanged(nameof(CaseList));
            }
        }

        private ObservableCollection<gpu> _gpuList;
        private List<gpu> _gpuAllList;
        public ObservableCollection<gpu> GpuList
        {
            get { return _gpuList; }
            set
            {
                _gpuList = value;
                OnPropertyChanged(nameof(GpuList));
            }
        }

        private ObservableCollection<ram> _ramList;
        private List<ram> _ramAllList;
        public ObservableCollection<ram> RamList
        {
            get { return _ramList; }
            set
            {
                _ramList = value;
                OnPropertyChanged(nameof(RamList));
            }
        }

        private ObservableCollection<powersupply> _powersupplyList;
        private List<powersupply> _powersupplyAllList;
        public ObservableCollection<powersupply> PowersupplyList
        {
            get { return _powersupplyList; }
            set
            {
                _powersupplyList = value;
                OnPropertyChanged(nameof(PowersupplyList));
            }
        }

        private ObservableCollection<processorcooler> _processorcoolerList;
        private List<processorcooler> _processorcoolerAllList;
        public ObservableCollection<processorcooler> ProcessorcoolerList
        {
            get { return _processorcoolerList; }
            set
            {
                _processorcoolerList = value;
                OnPropertyChanged(nameof(ProcessorcoolerList));
            }
        }

        private ObservableCollection<storagedevice> _storageList;
        private List<storagedevice> _storageAllList;
        public ObservableCollection<storagedevice> StorageList
        {
            get { return _storageList; }
            set
            {
                _storageList = value;
                OnPropertyChanged(nameof(StorageList));
            }
        }

        public void ClearKonfig()
        {
            Cpu = null;
            RAM = null;
            Motherboard = null;
            Case = null;
            GPU = null;
            Processorcooler = null;
            Powersupply = null;
            Storage = null;
        }
        public List<string> manufacturers { get; set; }

    }

    public static class MarWorkWith
    {
        
        public static WorkWithDB withDB { get; } = new WorkWithDB();

    }

}
