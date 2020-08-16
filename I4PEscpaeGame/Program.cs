
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;


namespace I4PEscpaeGame
{
    class Program
    {
        public static List<string> Invertory = new List<string>();


        static void Main(string[] args)
        {
            UserInteractions interactions = new UserInteractions(response: "", command: "", item1: "", item2: "", room: "nappali", Invertory);


            string[] parancsok = { "menj", "nézd", "veddfel", "teddle", "nyisd", "húzd", "törd" };
            string read = "";
            Console.WriteLine("Üdvözöllek a szabaduló szoba játékban!");
            Console.WriteLine();
            Console.WriteLine("A helyszín egy lakás a cél hogy kijuss. ");
            Console.WriteLine("A játék során a parancsok a következők:");

            foreach (var item in parancsok)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            Console.WriteLine("A menj parancs a észak, dél, kelet, nyugat használatával működik ");
            Console.WriteLine("Fontos, hogy egy parancsnál minding tegyél szóközt a szavak közé ezalól csak az összetett szavak képeznek kivételt pl:");
            Console.WriteLine("vedd fel / tedd le. =>  helyesen teddle, veddfel. ");
            Console.WriteLine("A tovább lépéshez nyomj egy entert!");
            Console.ReadLine();
            Console.Clear();

            while (interactions.Response != "Kijutottál!")
            {
                read = Console.ReadLine();
                if (read.Length > 1)
                {
                    interactions.Command = read.Split(' ')[0];
                    if (read.Split(' ').Length > 1)
                    {
                        interactions.Item1 = read.Split(' ')[1];
                    }
                    if (read.Split(' ').Length > 2)
                    {
                        interactions.Item2 = read.Split(' ')[2];
                    }
                }
                else
                {
                    Console.WriteLine("Adj meg egy parancsot!");
                }


                switch (interactions.Room)
                {
                    case "fürdő":
                        interactions.Response = Bath.BathOnGame(interactions, Invertory);
                        break;
                    default:
                        interactions.Response = Living.LivingOnGame(interactions, Invertory);
                        break;
                }

                Console.WriteLine(interactions.Response);


            }
            Console.WriteLine("A játék végetért!");
        }
    } 
}



