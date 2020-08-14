using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4PEscpaeGame
{
    class UserInteractions
    {
        public UserInteractions(string response, string command, string item1, string item2,  string room, List<string> invertory )
        {
            this.Response = response;
            this.Command = command;
            this.Item1 = item1;
            this.Item2 = item2;
            this.Room = room;
            this.Invertory = invertory;
        }

        public string Response { get; set; }

        public  string Command { get; set; }

        public string Item1 { get; set; }

        public string Item2 { get; set; }

        public string Room { get; set; }

        public List<string> Invertory { get; set; }

    }
}
