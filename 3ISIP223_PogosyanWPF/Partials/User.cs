using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    public partial class User
    {
        public string FIO => $"{FirstName} {LastName} {SecondName}";

        public string Uslugi => string.Join("\n - ", MasterSerives.Select(serv => serv.Service.TypeService.Name));
    }
}
