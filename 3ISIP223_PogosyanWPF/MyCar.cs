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

        //public double TotalPrice { get; set; } = 0;

        public MyCar(ColorOption color, Engine engine, CarModel model) 
        {
            DopOptinon = new List<DopOptinon>();
            Color = color;
            Engine = engine;
            Model = model;
        }

        public double CalculateTotalPrice()
        {
            double totalPrice = 0;

            totalPrice += Model.BasePrice + Color.Price + Engine.Price + DopOptinon.Sum(s => s.Price);

            return totalPrice;
        }


    }
}
