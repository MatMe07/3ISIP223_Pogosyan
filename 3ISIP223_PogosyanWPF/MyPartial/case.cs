using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    partial class @case
    {
        public string SupFormFactor => string.Join("/", boardformfactorcases.Select(s=> s.formfactor.name).ToList());

        public bool SovmestFormFactor(int id) => boardformfactorcases.FirstOrDefault(s => s.formfactorid == id) != null;
        public string Description
        {
            get
            {
                return $"{casesize.name}, {expansionslots} слотов, {SupFormFactor}" +
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
                return $"Корпус {basepart.manufacturer.name}";
            }
        }
    }
}
