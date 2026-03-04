using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    partial class powersupply
    {
        public string Description
        {
            get
            {
                return $"{basepart.name}, {power}W, {fandimension.name} fan, " +
                       $"{certificate.name}";
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
                return $"Блок питания {basepart.name}";
            }
        }

    }
}
