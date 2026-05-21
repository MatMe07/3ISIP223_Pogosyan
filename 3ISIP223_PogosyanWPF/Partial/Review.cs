using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    /// <summary>
    /// Частичный класс Review для добавления вычисляемых свойств
    /// </summary>
    public partial class Review
    {
        /// <summary>
        /// Причина заморозки отзыва (из одобренной жалобы)
        /// </summary>
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
