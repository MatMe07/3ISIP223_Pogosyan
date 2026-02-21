using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    partial class storagedevice
    {
        public string Description
        {
            get
            {
                string desc = $"{capacity / 1000}TB, {storagedeviceinterface.name}";

                if (storagedevicetype.name == "SSD") return $"{desc}, {ssd.tbw} TBW";
                else return $"{desc} {hdd.rotationspeed} об/мин";

            }
        }

        public string FullDescription
        {
            get
            {
                return $"{FullName}, {Description}";
            }
        }

        public string FullName
        {
            get
            {
                return $"Корпус {basepart.manufacturer.name} {basepart.name}";
            }
        }
    }
}
