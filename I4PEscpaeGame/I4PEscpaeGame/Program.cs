using I4PEscpaeGame.places.BathRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;


namespace I4PEscpaeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string response = "";
            string command = "";
            string item1 = "";
            string item2 = "";
            string read = "";
            while (response != "Kijutottál!")
            {
                read = Console.ReadLine();
                command = read.Split(' ')[0];
                item1 = read.Split(' ')[1];
                item2 = read.Split(' ')[2];
                response = Bath(command,item1,item2);
                Console.WriteLine(response);
            }
            Console.WriteLine("A játék végetért!");
        }

        private static string Bath(string command, string item1, string item2)
        {
            string response = "";
            List<ThingsBath> bathroomThings = new List<ThingsBath>();

            List<KeyValuePair<string, string>> kádFunctions = new List<KeyValuePair<string, string>>();
            kádFunctions.Add(new KeyValuePair<string, string>("nézd", "a kádban egy feszítővas van"));
            List<KeyValuePair<string, string>> feszítovasFunctions = new List<KeyValuePair<string, string>>();
            feszítovasFunctions.Add(new KeyValuePair<string, string>("nézd", "ez egy feszítő vas"));
            feszítovasFunctions.Add(new KeyValuePair<string, string>("vedd fel", "felvetted a feszítővasat"));
            feszítovasFunctions.Add(new KeyValuePair<string, string>("törd", "betörted a(z) "+item2+"-t" ));
            bathroomThings.Add(new ThingsBath(position: "kelet", name: "kád", isMooveable: false, kádFunctions));
            bathroomThings.Add(new ThingsBath(position: "Kád", name: "feszítő vas", isMooveable: false, feszítovasFunctions));
            switch (command)
            {
                case "menj":
                    switch (item1)
                    {
                        case "észak":

                            break;

                        case "dél":

                            break;
                        case "kelet":

                            break;
                        case "nyugat":

                            break;
                    }
                    break;
                
                default:
                        foreach (var thing in bathroomThings)
                        {
                            if (thing.Name==item1)
                            {
                                foreach (var funct in thing.Functions)
                                {
                                    if (command==funct.Key)
                                    {
                                        response = funct.Value;
                                    }
                                    else
                                    {
                                    response = "";
                                    }
                                }
                            }
                            else 
                            { 

                            }
                        }
                    break;
            }
            return response;
        }
    }
}
