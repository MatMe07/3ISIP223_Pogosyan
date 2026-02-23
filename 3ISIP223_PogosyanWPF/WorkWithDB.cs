using _3ISIP223_PogosyanWPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace _3ISIP223_PogosyanWPF
{
    public class WorkWithDB : INotifyPropertyChanged
    {
        public WorkWithDB() {
            //_cpu = null;

            _cpuList = new ObservableCollection<cpu>(Core.Context.cpus.ToList());
            _gpuList = new ObservableCollection<gpu>(Core.Context.gpus.ToList());
            _motherboardList = new ObservableCollection<motherboard>(Core.Context.motherboards.ToList());
            _powersupplyList = new ObservableCollection<powersupply>(Core.Context.powersupplies.ToList());
            _processorcoolerList = new ObservableCollection<processorcooler>( Core.Context.processorcoolers.ToList());
            _ramList = new ObservableCollection<ram>(Core.Context.rams.ToList());
            _caseList = new ObservableCollection<@case>(Core.Context.cases.ToList());
            _storageList = new ObservableCollection<storagedevice>(Core.Context.storagedevices.ToList());


            manufacturers = Core.Context.manufacturers.Select(s=>s.name).ToList();
            manufacturers.Insert(0, "Все");
            //manufacturers[0].name
            //CountConfig = 0;
            //OnPropertyChanged(nameof(CountConfig));
            //SumConfig = 0M;
            //OnPropertyChanged(nameof(SumConfig));

        }
        public int CountConfig
        {
            get
            {
                int coun = 0;
                if (Cpu != null) coun++;
                if (Motherboard != null) coun++;
                if (Case != null) coun++;
                if (RAM != null) coun++;
                if (Powersupply != null) coun++;
                if (Processorcooler != null) coun++;
                if (Storage != null) coun++;
                if (GPU != null) coun++;
                return coun;
            }
            set { }
        }


        public string IsSovmest(Object obj, ComponentType type)
        {
            string result = "";


            switch (type)
            {
                //case ComponentType.Motherboard:
                case ComponentType.CPU:
                    {
                        if( Motherboard!= null && (obj as cpu).socketid != Motherboard.socketid )
                        {
                            result = $"Процессор не совместим с материнской платой\nСокет процессора: {(obj as cpu).socket.name}\nСокет материнской платы: {Motherboard.socket.name}";
                            return result;
                        }
                        if( Processorcooler != null && !Processorcooler.SovmestSocket((obj as cpu).socketid))
                        {
                            result = $"Процессор не совместим с кулером\nСокет процессора: {(obj as cpu).socket.name}\nПоддерживаемые сокеты кулера: {Processorcooler.SocketDisp}";
                            return result;
                        }
                        break;
                    }






                case ComponentType.ProcessorCooler:
                    {
                        if ((Cpu!= null &&  
                            !(obj as processorcooler).SovmestSocket(Cpu.socketid))
                            || 
                            (Motherboard!=null &&
                            !(obj as processorcooler).SovmestSocket( Motherboard.socketid) )
                            )
                        {
                            result = $"Кулер не поддерживает сокет процессора/материнской платы\nСокет: {Cpu.socket.name}\nПоддерживаемые сокеты кулера: {(obj as processorcooler).SocketDisp}";
                            return result;
                        }
                        break;
                    }




                case ComponentType.Motherboard: {
                        if (Cpu != null && Cpu.socketid != (obj as motherboard).socketid)
                        {
                            result = $"Процессор не совместим с материнской платой\nСокет процессора: {Cpu.socket.name}\nСокет материнской платы: {(obj as motherboard).socket.name}";
                            return result;
                        }
                        else if (Processorcooler != null
                                &&
                                !Processorcooler.SovmestSocket((obj as motherboard).socketid))
                        {
                            result = $"Материнская плата не поддерживает сокет кулера\nСокет: {(obj as motherboard).socket.name}\nПоддерживаемые сокеты кулера: {Processorcooler.SocketDisp}";
                            return result;
                        }

                        else if (Case != null
                            &&
                            !Case.SovmestFormFactor((obj as motherboard).formfactorid))
                        {
                            result = "Материнская плата не помещается в выбранный корпус" +
                                $"\nФорм-фактор платы: {(obj as motherboard).formfactor.name}\nПоддерживаемые форм-факторы корпуса: {Case.SupFormFactor}";
                            return result;
                        }
                        else if (RAM != null
                            &&
                            (obj as motherboard).memorytypeid != RAM.memorytypeid
                            )
                        {
                            result = "Оперативная память не совместима с материнской платой" +
                                $"\nТип памяти платы: {(obj as motherboard).memorytype.name}\nТип памяти RAM: {RAM.memorytype.name}";
                            return result;
                        }

                            break;
                    }

                case ComponentType.Case:
                    {
                        if (Motherboard != null
                            &&
                            !(obj as @case).SovmestFormFactor(Motherboard.formfactorid)
                            )
                        {
                            result = "Материнская плата не помещается в выбранный корпус" +
                                $"\nФорм-фактор платы: {Motherboard.formfactor.name}\nПоддерживаемые форм-факторы корпуса: {(obj as @case).SupFormFactor}";
                            return result;
                        }
                        break;
                    }


                case ComponentType.RAM:
                    {
                        if(Motherboard!= null
                            &&
                            Motherboard.memorytypeid != (obj as ram).memorytypeid
                            )
                        {
                            result = "Оперативная память не совместима с материнской платой" +
                                $"\nТип памяти платы: {Motherboard.memorytype.name}\nТип памяти RAM: {(obj as ram).memorytype.name}";
                            return result;
                        }
                        break;
                    }



                case ComponentType.PowerSupply:
                    {
                        if (GPU!= null &&
                            (obj as powersupply).power < GPU.recommendpower
                            )
                        {
                            result = "Блок питания недостаточной мощности" +
                                $"\nРекомендуемая мощность для видеокарты: {GPU.recommendpower} W\nМощность блока питания: {(obj as powersupply).power} W";
                            return result;  
                        }

                        break;
                    }
                case ComponentType.GPU: {

                        if (Powersupply != null &&
    Powersupply.power < (obj as gpu).recommendpower
    )
                        {
                            result = "Блок питания недостаточной мощности" +
                                $"\nРекомендуемая мощность для видеокарты: {(obj as gpu).recommendpower} W\nМощность блока питания: {Powersupply.power} W";
                            return result;
                        }
                        break;
                    }

            }
            return "";
        }


        public double SumConfig
        {
            get=> Convert.ToDouble((Cpu?.basepart.price ?? 0) + (GPU?.basepart.price ?? 0) + (Motherboard?.basepart.price ?? 0) + (Case?.basepart.price ?? 0) + (Processorcooler?.basepart.price ?? 0) +
                    (RAM?.basepart.price ?? 0) + (Powersupply?.basepart.price ?? 0) + (Storage?.basepart.price ?? 0));
            set { }
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
                //CountConfig += value == null ? -1 : (_cpu != null ? 0 : 1);
                //OnPropertyChanged(name)
                _cpu = value;
                OnPropertyChanged(nameof(Cpu));
                OnPropertyChanged(nameof(SumConfig));
                OnPropertyChanged(nameof(CountConfig));
                
            }
        }
        private motherboard _motherboard;
        public motherboard Motherboard
        {
            get { return _motherboard; }
            set
            {
                //CountConfig += value == null ? -1 : (_motherboard != null ? 0 : 1);

                _motherboard = value;
                OnPropertyChanged(nameof(Motherboard));
                OnPropertyChanged(nameof(SumConfig));
                OnPropertyChanged(nameof(CountConfig));

            }
        }
        private @case _case;
        public @case Case
        {
            get { return _case; }
            set
            {
                //CountConfig += value == null ? -1 : (_case != null ? 0 : 1);
                _case = value;
                OnPropertyChanged(nameof(Case));
                OnPropertyChanged(nameof(SumConfig));
                OnPropertyChanged(nameof(CountConfig));

            }
        }
        private gpu _gpu;
        public gpu GPU
        {
            get { return _gpu; }
            set
            {
                //CountConfig += value == null ? -1 : (_gpu != null ? 0 : 1);
                _gpu = value;
                OnPropertyChanged(nameof(GPU));
                OnPropertyChanged(nameof(SumConfig));
                OnPropertyChanged(nameof(CountConfig));

            }
        }
        private ram _ram;
        public ram RAM
        {
            get { return _ram; }
            set
            {
                //CountConfig += value == null ? -1 : (_ram != null ? 0 : 1);
                _ram = value;
                OnPropertyChanged(nameof(RAM));
                OnPropertyChanged(nameof(SumConfig));
                OnPropertyChanged(nameof(CountConfig));

            }
        }
        private powersupply _powersupply;
        public powersupply Powersupply
        {
            get { return _powersupply; }
            set
            {
                //CountConfig += value == null ? -1 : (_powersupply != null ? 0 : 1);
                _powersupply = value;
                OnPropertyChanged(nameof(Powersupply));
                OnPropertyChanged(nameof(SumConfig));
                OnPropertyChanged(nameof(CountConfig));

            }
        }
        private processorcooler _processorcooler;
        public processorcooler Processorcooler
        {
            get { return _processorcooler; }
            set
            {
                //CountConfig += value == null ? -1 : (_processorcooler != null ? 0 : 1);
                _processorcooler = value;
                OnPropertyChanged(nameof(Processorcooler));
                OnPropertyChanged(nameof(SumConfig));
                OnPropertyChanged(nameof(CountConfig));

            }
        }
        private storagedevice _storagedevice;
        public storagedevice Storage
        {
            get { return _storagedevice; }
            set
            {
                //CountConfig += value == null ? -1 : (_storagedevice != null ? 0 : 1);
                _storagedevice = value;
                OnPropertyChanged(nameof(Storage));
                OnPropertyChanged(nameof(SumConfig));
                OnPropertyChanged(nameof(CountConfig));

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
                        var filterList = _cpuList.ToList();
                        filterList = filterList.Where(f => f.FullName.ToLower().Contains(search.ToLower())).ToList();
                        if (filter != "Все") filterList = filterList.Where(f => f.basepart.manufacturer.name == filter).ToList();
                        //CpuList.Clear();
                        //foreach (var item in filterList)
                        //{
                        //    CpuList.Add(item);
                        //}
                        CurrentList = new ObservableCollection<cpu> (filterList);
                        break;
                    }
                case ComponentType.GPU:
                    {
                        var filterList = _gpuList.ToList();
                        filterList = filterList.Where(f => f.basepart.name.ToLower().Contains(search.ToLower())).ToList();
                        if (filter != "Все") filterList = filterList.Where(f => f.basepart.manufacturer.name == filter).ToList();
                        //CpuList.Clear();
                        //foreach (var item in filterList)
                        //{
                        //    CpuList.Add(item);
                        //}
                        CurrentList = new ObservableCollection<gpu>(filterList);
                        break;
                    }
                case ComponentType.Motherboard:
                    {
                        var filterList = _motherboardList.ToList();
                        filterList = filterList.Where(f => f.basepart.name.ToLower().Contains(search.ToLower())).ToList();
                        if (filter != "Все") filterList = filterList.Where(f => f.basepart.manufacturer.name == filter).ToList();
                        //CpuList.Clear();
                        //foreach (var item in filterList)
                        //{
                        //    CpuList.Add(item);
                        //}
                        CurrentList = new ObservableCollection<motherboard>(filterList);
                        break;
                    }
                case ComponentType.ProcessorCooler:
                    {
                        var filterList = _processorcoolerList.ToList();
                        filterList = filterList.Where(f => f.basepart.name.ToLower().Contains(search.ToLower())).ToList();
                        if (filter != "Все") filterList = filterList.Where(f => f.basepart.manufacturer.name == filter).ToList();
                        //CpuList.Clear();
                        //foreach (var item in filterList)
                        //{
                        //    CpuList.Add(item);
                        //}
                        CurrentList = new ObservableCollection<processorcooler>(filterList);
                        break;
                    }
                case ComponentType.Case:
                    {
                        var filterList = _caseList.ToList();
                        filterList = filterList.Where(f => f.basepart.name.ToLower().Contains(search.ToLower())).ToList();
                        if (filter != "Все") filterList = filterList.Where(f => f.basepart.manufacturer.name == filter).ToList();
                        //CpuList.Clear();
                        //foreach (var item in filterList)
                        //{
                        //    CpuList.Add(item);
                        //}
                        CurrentList = new ObservableCollection<@case>(filterList);
                        break;
                    }
                case ComponentType.PowerSupply:
                    {
                        var filterList = _powersupplyList.ToList();
                        filterList = filterList.Where(f => f.basepart.name.ToLower().Contains(search.ToLower())).ToList();
                        if (filter != "Все") filterList = filterList.Where(f => f.basepart.manufacturer.name == filter).ToList();
                        //CpuList.Clear();
                        //foreach (var item in filterList)
                        //{
                        //    CpuList.Add(item);
                        //}
                        CurrentList = new ObservableCollection<powersupply>(filterList);
                        break;
                    }
                case ComponentType.RAM:
                    {
                        var filterList = _ramList.ToList();
                        filterList = filterList.Where(f => f.basepart.name.ToLower().Contains(search.ToLower())).ToList();

                        if (filter != "Все") filterList = filterList.Where(f => f.basepart.manufacturer.name == filter).ToList();
                        //CpuList.Clear();
                        //foreach (var item in filterList)
                        //{
                        //    CpuList.Add(item);
                        //}
                        CurrentList = new ObservableCollection<ram>(filterList);
                        break;
                    }
                case ComponentType.Storage:
                    {
                        var filterList = _storageList.ToList();
                        filterList = filterList.Where(f => f.basepart.name.ToLower().Contains(search.ToLower())).ToList();
                        if (filter != "Все") filterList = filterList.Where(f => f.basepart.manufacturer.name == filter).ToList();
                        //CpuList.Clear();
                        //foreach (var item in filterList)
                        //{
                        //    CpuList.Add(item);
                        //}
                        CurrentList = new ObservableCollection<storagedevice>(filterList);
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
        //private List<cpu> _cpuAllList;
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
        //private List<motherboard> _motherboardAllList;
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
        //private List<@case> _caseAllList;
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
        //private List<gpu> _gpuAllList;
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
        //private List<ram> _ramAllList;
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
        //private List<powersupply> _powersupplyAllList;
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
        //private List<processorcooler> _processorcoolerAllList;
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
        //private List<storagedevice> _storageAllList;
        public ObservableCollection<storagedevice> StorageList
        {
            get { return _storageList; }
            set
            {
                _storageList = value;
                OnPropertyChanged(nameof(StorageList));
            }
        }


        private object _currentList;
        public object CurrentList
        {
            get { return _currentList; }
            set
            {
                _currentList = value;
                OnPropertyChanged(nameof(CurrentList));
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
