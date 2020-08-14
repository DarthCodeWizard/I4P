using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace I4PEscpaeGame.places.Livingroom
{
    class ThingsLiving
    {
        public ThingsLiving(string position, string name, bool isMooveable, List<KeyValuePair<string, string>> functions)
        {
            IsMooveable = isMooveable;
            Functions = functions;
            Name = name;
            Position = position;
        }

       
        public string Name { get; set; }

        public bool IsMooveable { get; set; }

        public List<KeyValuePair<string, string>> Functions { get; set; }

        public string Position { get; set; }

    }
}
