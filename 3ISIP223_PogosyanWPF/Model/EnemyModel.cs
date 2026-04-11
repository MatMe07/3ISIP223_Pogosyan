using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF.Model.Units
{
    public class EnemyModel : INotifyPropertyChanged
    {
        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImagePath)));
            }
        }

        public AxisAngleRotation3D Rotation { get; set; }
        public TranslateTransform3D PositionTransform { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
