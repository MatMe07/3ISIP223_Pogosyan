using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _3ISIP223_PogosyanWPF
{
    internal class CarConfig
    {
        private static CarConfig _instance = new CarConfig();

        public List<ColorOption> colors = new List<ColorOption> {
            new ColorOption("Blue", 0),
            new ColorOption("Red", 5000),
            new ColorOption("Black", 3000),
            new ColorOption("Wite", 2000),
        };
        public List<CarModel> carModels = new List<CarModel> { 
            new CarModel(
                "BMW",
                 new List<Engine> {
                        new Engine("S58 Twin-Turbo", 0),
                        new Engine("S58 M Driver's", 300_000),
                        new Engine("S58 Competition", 500_000),
                 },
                 8_100_000
                ),
            new CarModel(
                "Camaro",
                 new List<Engine> {
                        new Engine("2.0L Turbo I4", 0),
                        new Engine("3.6L V6", 200_000),
                        new Engine("6.2L V8 LT1", 500_000),
                 },
                 2_000_000
                ),
            new CarModel(
                "Bugatti",
                 new List<Engine> {
                        new Engine("W16 8.0 Quad-Turbo", 0),
                        new Engine("W16 8.0 Super Sport", 1_500_000),
                        new Engine("Pur Sport", 2_000_000),
                 },
                 153_000_000
                ),
        };
        
        public List<DopOptinon> dopOptinons = new List<DopOptinon>
        {
            new DopOptinon("Персональная гравировка на порогах", 1_500_000),
            new DopOptinon("Карбон-керамические тормоза", 800_000),
            new DopOptinon("Режим отслеживания", 200_000),
            new DopOptinon("Коврики с подсветкой", 40_000),
        };
             
        public MyCar Car { get; set; }
        private CarConfig() { } 
        public static CarConfig Instance
            { get { return _instance; } }
    }
}
