using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//using Windows;
using System.Windows;


namespace _3ISIP223_PogosyanWPF
{
    public partial class StatusesRequest
    {
        public string GetColorStatus
        {
            get
            {
                switch (Name)
                {
                    case "Одобрена":
                        {
                            return "#4CAF50";

                        }
                    case "Отклонена":
                        {
                            return "#F44336";
                        }
                    default:
                        {
                            return "#FF9800";

                        }
                }
            }
            set { }
        }
    }

}
