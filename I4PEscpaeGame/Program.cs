
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


                switch (interactions.Room)
                {
                    case "fürdő":
                        interactions.Response = Bath(interactions);
                        break;
                    default:
                        interactions.Response = Living(interactions);
                        break;
                }
                
                    Console.WriteLine(interactions.Response);
                

            }
            Console.WriteLine("A játék végetért!");
        }

        private static string Living(UserInteractions interactions)
        {

            string temp="";
            List<Thing> LivingRoomThings = new List<Thing>();
      


            List<KeyValuePair<string, string>> ágyFunctions = new List<KeyValuePair<string, string>>();

            ágyFunctions.Add(new KeyValuePair<string, string>("nézd"," Ez egy ágy."));


            List<KeyValuePair<string, string>> szekrényFunctions = new List<KeyValuePair<string, string>>();

            szekrényFunctions.Add(new KeyValuePair<string, string>("nézd", "Ez egy szekrény ami be van csukva."));
            szekrényFunctions.Add(new KeyValuePair<string, string>("nyisd", "kinyitottad a szekrényt amiben egy doboz található."));
            szekrényFunctions.Add(new KeyValuePair<string, string>("húzd", "elúztad a szekrényt és mögötte egy ablakot látsz."));


            List<KeyValuePair<string, string>> dobozFunctions = new List<KeyValuePair<string, string>>();
            dobozFunctions.Add(new KeyValuePair<string, string>("veddfel", "felvetted a dobozt"));
            dobozFunctions.Add(new KeyValuePair<string, string>("nyisd", "kinyitottad a dobozt és egy kulcs található benne"));
            dobozFunctions.Add(new KeyValuePair<string, string>("teddle", "le tetted a dobozt"));
            dobozFunctions.Add(new KeyValuePair<string, string>("nézd", "ez egy csukott doboz"));


            List<KeyValuePair<string, string>> kulcsFunctions = new List<KeyValuePair<string, string>>();
            kulcsFunctions.Add(new KeyValuePair<string, string>("veddfel", "Felvetted a dobozt."));
            kulcsFunctions.Add(new KeyValuePair<string, string>("teddle", "Le tetted a dobozt."));
            kulcsFunctions.Add(new KeyValuePair<string, string>("nézd", "Ez egy kulcs valószínüleg egy ajtóhoz való."));

            List<KeyValuePair<string, string>> ajtoFunctions = new List<KeyValuePair<string, string>>();
            ajtoFunctions.Add(new KeyValuePair<string, string>("nyisd", "az ajtó kinyilt nyugatra a fürdőszoba van előtted"));
            



            LivingRoomThings.Add(new Thing(position: "kelet", name: "ágy", isMooveable: false, ágyFunctions, isOpenable: false, isOpen: false));
            LivingRoomThings.Add(new Thing(position: "észak", name: "szekrény", isMooveable: false, szekrényFunctions, isOpenable: true, isOpen:false));
            LivingRoomThings.Add(new Thing(position: "nyugat", name: "ajtó", isMooveable: false, ajtoFunctions , isOpenable: true, isOpen: false));
            LivingRoomThings.Add(new Thing(position: "doboz", name: "kulcs", isMooveable: true, kulcsFunctions, isOpenable: false, isOpen: false));
            LivingRoomThings.Add(new Thing(position: "szekrény", name: "doboz", isMooveable: true, dobozFunctions, isOpenable: true, isOpen: false));
            
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
                                interactions.Room = "fürdő";
                            }
                            else
                            {
                                interactions.Response = "Itt egy ajtó van amin nem tudsz átmenni mert zárva van.";
                            }

                            break;
                    }
                    break;
             
                default:

                    foreach (var thing in LivingRoomThings)
                    {
                        if (interactions.Item1 == thing.Name)
                        {
                            foreach (var funct in thing.Functions)
                            {
                                if (thing.IsOpenable && interactions.Command == "nyisd" && Invertory.Contains("kulcs") && interactions.Item1 == "ajtó" && interactions.Item2 == "kulcs")
                                {
                                    thing.IsOpen = true;
                                    interactions.Response = funct.Value;
                                }

                                else if (thing.IsOpenable && interactions.Command == "nyisd" &&interactions.Command==funct.Key)
                                {
                                    thing.IsOpen = true;
                                }

                                if (interactions.Command == funct.Key)
                                {
                                    temp=funct.Value.ToString();
                                    interactions.Response = temp;
                                    

                                   
                                    if (thing.IsMooveable && interactions.Command=="veddfel")
                                    {
                                        Invertory.Add(thing.Name);
                                    }
                                    else if (thing.IsMooveable && interactions.Command == "teddle")
                                    {
                                        Invertory.Remove(thing.Name);
                                   
                                    }
                                   
                                }
                             
                            }
                            if (temp=="")
                            {
                                switch (interactions.Command)
                                {
                                    case "nyisd":
                                        interactions.Response = " A(z) " + interactions.Item1 + " tárgyat nem tudom kinyitni";
                                        break;
                                    case "törd":
                                        interactions.Response = " A(z) " + interactions.Item1 + " tárgygyal nem tudok törni";
                                        break;
                                    case "veddfel":
                                        interactions.Response = " A(z) " + interactions.Item1 + " tárgygyat nem látom";
                                        break;
                                    case "húzd":
                                        interactions.Response = " A(z) " + interactions.Item1 + " tárgygyat nem tudom húzni";
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
                                        interactions.Response = " A(z) " + interactions.Item2 + " tárgyat nem tudom kinyitni";
                                        break;
                                    case "törd":
                                        interactions.Response = " A(z) " + interactions.Item2 + " tárgygyal nem tudok törni";
                                        break;
                                    case "veddfel":
                                        interactions.Response = " A(z) " + interactions.Item2 + " tárgygyat nem látom";
                                        break;
                                    case "húzd":
                                        interactions.Response = " A(z) " + interactions.Item2 + " tárgygyat nem tudom húzni";
                                        break;
                                    default:

                                        break;
                                }

                            }
                        }

                    }
                    break;



            }
            if (interactions.Command=="nézd"&&interactions.Item1=="" && interactions.Item2=="")
            {
                interactions.Response = "A nappaliban vagy itt található egy szekrény. Nyugatra látsz egy ajtót. ";
            }
            return interactions.Response;
        }
    

        private static string Bath(UserInteractions interactions)
        {
            
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
                            interactions.Response = "Északnak nem tudsz menni, arra nincs kijárat.";
                            break;

                        case "dél":
                            interactions.Response = "Délnek nem tudsz menni, arra nincs kijárat.";
                            break;
                        case "kelet":
                            interactions.Response = "A nappaliban vagy.";
                            break;
                        case "nyugat":
                            interactions.Response = "Délnek nem tudsz menni, arra nincs kijárat.";
                            break;
                    }
                    break;

                    case "nézd":
                    interactions.Response = "A szobában keletre egy kádat látok, a kádon kívül nincs semmi ebben a helységben. ";
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
                                    interactions.Response = funct.Value;
                                    }
                                    switch (interactions.Command)
                                    {
                                       

                                        case "nyisd":
                                        interactions.Response = " A(z) " + interactions.Item1 + " tárgyat nem tudom kinyitni.";
                                            break;

                                        case "törd":
                                        interactions.Response = " A(z) " + interactions.Item1 + " tárgygyal nem tudok törni.";
                                            break;

                                        case "veddfel":
                                        interactions.Response = " A(z) " + interactions.Item1 + " tárgygyat nem látom.";
                                            break;

                                        case "húzd":
                                        interactions.Response = " A(z) " + interactions.Item1 + " tárgygyat nem tudom húzni.";
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
                                    interactions.Response = funct.Value;
                                    }
                                switch (interactions.Command)
                                {
                                    case "nyisd":
                                        interactions.Response = " A(z) " + interactions.Item2 + " tárgyat nem tudom kinyitni";
                                        break;
                                    case "törd":
                                        interactions.Response = " A(z) " + interactions.Item2 + " tárgygyal nem tudok törni";
                                        break;
                                    case "veddfel":
                                        interactions.Response = " A(z) " + interactions.Item2 + " tárgygyat nem látom";
                                        break;
                                    case "húzd":
                                        interactions.Response = " A(z) " + interactions.Item2 + " tárgygyat nem tudom húzni";
                                        break;
                                    default:
                                     
                                        break;
                                }

                            }
                            }
                           
                        }
                    break;



            }
           
            return interactions.Response;
        }
    }
}



