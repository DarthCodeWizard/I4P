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
        public LoadedData(string name, bool isOpen, bool isChecked)
        {
            Name = name;
          
            IsOpen = isOpen;
           
            IsChecked = isChecked;
            
        }

        public string Name { get; set; }
       
        public bool IsOpen { get; set; }
      
        public bool IsChecked { get; set; }

    }
}
