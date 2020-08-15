using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4PEscpaeGame
{
    class Thing
    {

        public Thing( string position ,string name, bool isMooveable, List<KeyValuePair<string, string>> functions, bool isOpenable, bool isOpen)
        {
            IsMooveable = isMooveable;
            Functions = functions;
            Name = name;
            Position = position;
            IsOpen = isOpen;
            IsOpenable = isOpenable;
        }
        public string Name { get; set; }

        public bool IsMooveable { get; set; }

        public List<KeyValuePair<string, string>>Functions { get; set; }

        public string Position { get; set; }

        public bool IsOpenable { get; set; }

        public  bool IsOpen { get; set; }
    }
}
