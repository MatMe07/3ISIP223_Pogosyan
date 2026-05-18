using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    public partial class Review
    {
        //public double RatingFive => Rating / 2.0;
        public string FrozenReason
        {
            get
            {
                var comb = Complaints.FirstOrDefault(c => c.StatusesRequest.Name == "Одобрена");
                if (comb != null)
                {
                    return comb.Reason.Name;
                }
                else
                {
                    return "Причина не указана";
                }
            }
            set { }
        }
    }
}
