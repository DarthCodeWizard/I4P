
using System;
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
        public static UserInteractions interactions = new UserInteractions(response: "", command: "", item1: "", item2: "", room: "", Invertory);
       
        
        static void Main(string[] args)
        {
           
         
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

            while (interactions.Response != "Kijutottál!")
            {
                read = Console.ReadLine();
                if (read.Length>1)
                {
                    interactions.Command = read.Split(' ')[0];
                    if (read.Split(' ').Length>1)
                    {
                        interactions.Item1 = read.Split(' ')[1];
                    }
                    if (read.Split(' ').Length >2)
                    {
                        interactions.Item2 = read.Split(' ')[2];
                    }
                }
                else
                {
                    Console.WriteLine("Adj meg egy parancsot!");
                }
              

              
                while (interactions.Room=="fürdő")
                {
                    interactions.Response = Bath(interactions);
                }

                while (interactions.Room == "nappali")
                {
                    interactions.Response = Living(interactions);
                }
               
                Console.WriteLine(interactions.Response);
            }
            Console.WriteLine("A játék végetért!");
        }

        private static string Living(UserInteractions interactions)
        {

            string response = "";
            List<Thing> LivingRoomThings = new List<Thing>();
      


            List<KeyValuePair<string, string>> ágyFunctions = new List<KeyValuePair<string, string>>();

            ágyFunctions.Add(new KeyValuePair<string, string>("nézd"," Ez egy ágy."));


            List<KeyValuePair<string, string>> szekrényFunctions = new List<KeyValuePair<string, string>>();

            szekrényFunctions.Add(new KeyValuePair<string, string>("nézd", "Ez egy szekrény ami be van csukva."));
            szekrényFunctions.Add(new KeyValuePair<string, string>("nyisd", "kinyitottad a szekrényt amiben egy doboz található."));
            szekrényFunctions.Add(new KeyValuePair<string, string>("húzd", "elúztad a szekrényt és mögötte egy ablakot látsz."));
            
            LivingRoomThings.Add(new Thing(position: "kelet", name: "ágy", isMooveable: false, ágyFunctions, isOpenable: false, isOpen: false));
            LivingRoomThings.Add(new Thing(position: "észak", name: "szekrény", isMooveable: false, szekrényFunctions, isOpenable: true, isOpen:false));

            switch (interactions.Command)
            {
                case "menj":
                    switch (interactions.Item1)
                    {
                        case "észak":

                            break;

                        case "dél":

                            break;
                        case "kelet":

                            break;
                        case "nyugat":
                            var ajtó = (from x in LivingRoomThings where (x.Name == "ajtó") select (x)).FirstOrDefault();
                            if (ajtó.IsOpen)
                            {
                                interactions.Response = "A fürdőben vagy.";
                            }
                            else
                            {
                                interactions.Response = "Itt egy ajtó van amin nem tudsz átmenni mert zárva van.";
                            }

                            break;
                    }
                    break;
                case "nézd":
                    response = "A szobában keletre egy kádat látok, a kádon kívül nincs semmi ebben a helységben. ";
                    break;
                default:

                    foreach (var thing in LivingRoomThings)
                    {
                        if (thing.Name == interactions.Item1)
                        {
                            foreach (var funct in thing.Functions)
                            {
                                if (interactions.Command == funct.Key)
                                {
                                    response = funct.Value;

                                    if (thing.IsOpenable && interactions.Command=="nyisd" && Invertory.Contains("kulcs") && interactions.Item2 == "kulcs")
                                    {
                                        thing.IsOpen = true;
                                    }
                                    if (thing.IsMooveable && interactions.Command=="vedd fel")
                                    {
                                        Invertory.Add(thing.Name);
                                    }
                                }
                                
                                switch (interactions.Command)
                                {
                                    case "nyisd":
                                        response = " A(z) " + interactions.Item1 + " tárgyat nem tudom kinyitni";
                                        break;
                                    case "törd":
                                        response = " A(z) " + interactions.Item1 + " tárgygyal nem tudok törni";
                                        break;
                                    case "veddfel":
                                        response = " A(z) " + interactions.Item1 + " tárgygyat nem látom";
                                        break;
                                    case "húzd":
                                        response = " A(z) " + interactions.Item1 + " tárgygyat nem tudom húzni";
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else if (thing.Name == interactions.Item2)
                        {

                            foreach (var funct in thing.Functions)
                            {
                                if (interactions.Command == funct.Key)
                                {
                                    switch (funct.Key)
                                    {

                                        default:
                                            break;
                                    }
                                }
                                switch (interactions.Command)
                                {
                                    case "nyisd":
                                        response = " A(z) " + interactions.Item2 + " tárgyat nem tudom kinyitni";
                                        break;
                                    case "törd":
                                        response = " A(z) " + interactions.Item2 + " tárgygyal nem tudok törni";
                                        break;
                                    case "veddfel":
                                        response = " A(z) " + interactions.Item2 + " tárgygyat nem látom";
                                        break;
                                    case "húzd":
                                        response = " A(z) " + interactions.Item2 + " tárgygyat nem tudom húzni";
                                        break;
                                    default:

                                        break;
                                }

                            }
                        }

                    }
                    break;



            }
            interactions.Response = response;
            return interactions.Response;
        }
    

        private static string Bath(UserInteractions interactions)
        {
            string response = "";
            List<Thing> bathroomThings = new List<Thing>();

            List<KeyValuePair<string, string>> kádFunctions = new List<KeyValuePair<string, string>>();
            kádFunctions.Add(new KeyValuePair<string, string>("nézd", "a kádban egy feszítővas van"));


            List<KeyValuePair<string, string>> feszítovasFunctions = new List<KeyValuePair<string, string>>();

            feszítovasFunctions.Add(new KeyValuePair<string, string>("nézd", "ez egy feszítő vas"));
            feszítovasFunctions.Add(new KeyValuePair<string, string>("veddfel", "felvetted a feszítővasat"));
            feszítovasFunctions.Add(new KeyValuePair<string, string>("törd", "betörted a(z) " + interactions.Item2 + "-t"));


            bathroomThings.Add(new Thing(position: "kelet", name: "kád", isMooveable: false, kádFunctions,isOpenable:false, isOpen:false));
            bathroomThings.Add(new Thing(position: "kád", name: "feszítő vas", isMooveable: false, feszítovasFunctions, isOpenable:false, isOpen:false));

            switch (interactions.Command)
            {
                case "menj":
                    switch (interactions.Item1)
                    {
                        case "észak":
                            response = "Északnak nem tudsz menni, arra nincs kijárat.";
                            break;

                        case "dél":
                            response = "Délnek nem tudsz menni, arra nincs kijárat.";
                            break;
                        case "kelet":
                            response = "A nappaliban vagy.";
                            break;
                        case "nyugat":
                            response = "Délnek nem tudsz menni, arra nincs kijárat.";
                            break;
                    }
                    break;

                    case "nézd":
                        response = "A szobában keletre egy kádat látok, a kádon kívül nincs semmi ebben a helységben. ";
                        break;
                default:
                    
                        foreach (var thing in bathroomThings)
                        {
                            if (thing.Name == interactions.Item1)
                            {
                                foreach (var funct in thing.Functions)
                                {
                                    if (interactions.Command == funct.Key)
                                    {
                                        response = funct.Value;
                                    }
                                    switch (interactions.Command)
                                    {
                                       

                                        case "nyisd":
                                            response = " A(z) " + interactions.Item1 + " tárgyat nem tudom kinyitni.";
                                            break;

                                        case "törd":
                                            response = " A(z) " + interactions.Item1 + " tárgygyal nem tudok törni.";
                                            break;

                                        case "veddfel":
                                            response = " A(z) " + interactions.Item1 + " tárgygyat nem látom.";
                                            break;

                                        case "húzd":
                                            response = " A(z) " + interactions.Item1 + " tárgygyat nem tudom húzni.";
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                            else if (thing.Name == interactions.Item2)
                            {

                                foreach (var funct in thing.Functions)
                                {
                                    if (interactions.Command == funct.Key)
                                    {
                                        response = funct.Value;
                                    }
                                switch (interactions.Command)
                                {
                                    case "nyisd":
                                        response = " A(z) " + interactions.Item2 + " tárgyat nem tudom kinyitni";
                                        break;
                                    case "törd":
                                        response = " A(z) " + interactions.Item2 + " tárgygyal nem tudok törni";
                                        break;
                                    case "veddfel":
                                        response = " A(z) " + interactions.Item2 + " tárgygyat nem látom";
                                        break;
                                    case "húzd":
                                        response = " A(z) " + interactions.Item2 + " tárgygyat nem tudom húzni";
                                        break;
                                    default:
                                     
                                        break;
                                }

                            }
                            }
                           
                        }
                    break;



            }
            interactions.Response = response;
            return interactions.Response;
        }
    }
}



