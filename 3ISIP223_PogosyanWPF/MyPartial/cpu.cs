using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    partial class cpu
    {
        public string Description
        {
            get
            {
                return $"{numberofcores} ядер, {basecorefrequency}/{maxcorefrequency} ГГц, " +
                    $"{cachel3}MB L3, " +
                      $"{thermalpower}W TDP, " +
                      $"{socket.name}";
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
                return $"Процессор {basepart.name}";
            }
        }

        
    }
}
