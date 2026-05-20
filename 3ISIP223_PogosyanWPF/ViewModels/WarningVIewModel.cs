using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class WarningVIewModel: BaseViewModel
    {
        public WarningVIewModel()
        {
            User = dataBase.User;
        }

        public bool CheckIsUnfreezeReq()
        {
            return dataBase.CheckIsUnfreezeReq();
        }

        public User User { get; set; }
    }
}
