using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    partial class motherboard
    {
        public string Description
        {
            get
            {
                return $"{formfactor.name}, {socket.name}, {memoryslots}×{memorytype.name}, " +
                       $"{pcislots}×PCIe, {sataports}×SATA, {usbports}×USB";
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
                return $"Материнская плата {basepart.manufacturer.name} {basepart.name}";
            }
        }
    }
}
