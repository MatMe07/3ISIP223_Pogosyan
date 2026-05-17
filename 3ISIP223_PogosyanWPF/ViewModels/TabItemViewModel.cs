using Microsoft.Xaml.Behaviors.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class TabItemViewModel
    {
        public string HeaderText { get; set; }
        public string IconKind { get; set; }
        public object Tag { get; set; }

        public double Width { get; set; }
    }
}
