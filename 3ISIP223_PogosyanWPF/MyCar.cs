using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _3ISIP223_PogosyanWPF
{
    internal class MyCar
    {
        public  CarModel Model {  get; set; }

        public Engine Engine {  get; set; }
        public  ColorOption Color { get; set; }
        public string PathImage => $"Img/{Model.Name}{Color.Name}.png";
        public  List<DopOptinon> DopOptinon { get; set; }

        public  decimal TotalPrice { get; set; }


    }
}
