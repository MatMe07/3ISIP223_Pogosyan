using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    partial class gpu
    {
        public string VidConnectors => string.Join(", ", videoconnectorgpus.Select(s=>s.videoconnector.name));

        public string Description
        {
            get
            {
                return $"{videomemory}GB, {chipfrequency} МГц, {memorybus}-bit, {VidConnectors}" +
                       $"{recommendpower.Value}W";
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
                return $"Видеокарта {basepart.manufacturer.name}";
            }
        }
    }
}
