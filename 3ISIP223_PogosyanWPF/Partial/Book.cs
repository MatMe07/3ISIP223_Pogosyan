using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_PogosyanWPF
{
    public partial class Book
    {
        //public double RatingFive => (Rating ?? 0) / 2.0;
        public string FrozenReason
        {
            get
            {
                var comb = Complaints.FirstOrDefault(c=>c.StatusesRequest.Name == "Одобрена");
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
