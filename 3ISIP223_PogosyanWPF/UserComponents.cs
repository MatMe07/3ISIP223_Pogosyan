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
        public ObservableCollection<basepart> baseparts { get; set; }
        public string FullAllName {  get; set; }
        public decimal TotalSum { get; set; }
        public assembly Name { get; set; }
        //public List<string> imgParts;
        
        public UserComponents(List<basepart> bs, assembly name)
        {
            //baseparts = new List<basepart>();
            baseparts = new ObservableCollection<basepart>( bs);
            //imgParts = bs.Select(s => s.image).ToList();
            FullAllName = string.Join(" | ", baseparts.Select(s=>s.name));
            TotalSum = baseparts.Sum(s=>s.price);
            
            Name = name;
        }




    }
}
