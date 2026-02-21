using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    partial class processorcooler
    {
        public string SocketDisp => string.Join(", ", socketprocessorcoolers.Select(s=>s.socket.name));

        public string Description
        {
            get
            {
                return $"{fandimension.name} вентилятор, {heatpipes} теплотрубок, {minspeed}-{maxspeed} об/мин, " +
                       $"{noiselevel} дБ, {SocketDisp}";
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
                return $"Кулер {basepart.manufacturer.name} {basepart.name}";
            }
        }
    }
}
