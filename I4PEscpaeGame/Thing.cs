using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4PEscpaeGame
{
    class Thing
    {

        public Thing( bool breakable,bool isInSomething ,string container, string name, bool isMooveable, List<KeyValuePair<string, string>> functions,
            bool isOpenable, bool isOpen, bool isChecked, bool isPullable )
        {
            IsMooveable = isMooveable;
            Functions = functions;
            Name = name;
            Container = container;
            Breakable = breakable;
            IsChecked = isChecked;
            IsPulleable = isPullable;
            IsInSomething = isInSomething;
            IsOpen = isOpen;
            IsOpenable = isOpenable;
        }
        public string Name { get; set; }

        public bool IsMooveable { get; set; }

        public List<KeyValuePair<string, string>>Functions { get; set; }

        public bool IsOpenable { get; set; }

        public  bool IsOpen { get; set; }
        public bool IsInSomething { get; set; }
        public  string Container { get; set; }

        public bool Breakable { get; set; }
        public bool IsChecked { get; set; }
        public bool IsPulleable { get; set; }
    }
}
