using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    partial class ram
    {
        public string Description
        {
            get
            {
                return $"{capacity * count}GB ({count}x{capacity}GB), {memorytype.name}-{ghz}, {timings}";
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
                return $"Оперативная память {basepart.manufacturer.name} {basepart.name}";
            }
        }
    }
}
