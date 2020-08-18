using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace I4PEscpaeGame
{
    class LoadedData
    {
        public LoadedData(string name, bool isOpenable, bool isOpen, bool isMooveable, bool isInSomething, bool isBreakable, bool isChecked, bool isPulleable)
        {
            Name = name;
            IsOpenable = isOpenable;
            IsOpen = isOpen;
            IsMooveable = isMooveable;
            IsInSomething = isInSomething;
            IsBreakable = isBreakable;
            IsChecked = isChecked;
            IsPulleable = isPulleable;
        }

        public string Name { get; set; }
        public bool IsOpenable { get; set; }
        public bool IsOpen { get; set; }
        public bool IsMooveable { get; set; }
        public bool IsInSomething { get; set; }
        public bool IsBreakable { get; set; }
        public bool IsChecked { get; set; }
        public bool IsPulleable { get; set; }
    }
}
