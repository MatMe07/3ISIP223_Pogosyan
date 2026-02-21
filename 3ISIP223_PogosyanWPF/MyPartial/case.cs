using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    partial class @case
    {
        public string SupFormFactor => string.Join("/", boardformfactorcases.Where(s=> s.formfactor.id == s.id).ToList());
        public string Description
        {
            get
            {
                return $"{casesize}, {expansionslots} слотов, {SupFormFactor}" +
                       $"{fans} вентилятора";
            }
        }

        public string FullDescription
        {
            get
            {
                return $"{FullName}, {Description}";
            }
        }

        public string FullName
        {
            get
            {
                return $"Корпус {basepart.manufacturer.name} {basepart.name}";
            }
        }
    }
}
