using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    public class UserComponents
    {
        public ObservableCollection<basepart> baseparts;
        public List<string> imgParts;
        
        public UserComponents(List<basepart> bs)
        {
            //baseparts = new List<basepart>();
            baseparts = new ObservableCollection<basepart>( bs);
            imgParts = bs.Select(s => s.image).ToList();
        }



    }
}
