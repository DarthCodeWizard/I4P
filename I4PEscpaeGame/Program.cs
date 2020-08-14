
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
            List<string> Invertory = new List<string>();
            UserInteractions interactions = new UserInteractions(response: "",command:"",item1:"",item2:"", room:"", Invertory);
            
            string read = "";
         
            while (interactions.Response != "Kijutottál!")
            {
                read = Console.ReadLine();
               interactions.Command = read.Split(' ')[0];
                interactions.Item1 = read.Split(' ')[1];
                interactions.Item2 = read.Split(' ')[2];

                if (interactions.Response == "A fürdőszobában vagy.")
                {
                    interactions.Room = "fürdő";
                }
                else
                    interactions.Room = "nappali";
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

            List<KeyValuePair<string, string>> kádFunctions = new List<KeyValuePair<string, string>>();
            kádFunctions.Add(new KeyValuePair<string, string>("nézd", "a kádban egy feszítővas van"));
            List<KeyValuePair<string, string>> feszítovasFunctions = new List<KeyValuePair<string, string>>();
            feszítovasFunctions.Add(new KeyValuePair<string, string>("nézd", "ez egy feszítő vas"));
            feszítovasFunctions.Add(new KeyValuePair<string, string>("veddfel", "felvetted a feszítővasat"));
            feszítovasFunctions.Add(new KeyValuePair<string, string>("törd", "betörted a(z) " + interactions.Item2 + "-t"));
            LivingRoomThings.Add(new Thing(position: "kelet", name: "kád", isMooveable: false, kádFunctions));
            LivingRoomThings.Add(new Thing(position: "kád", name: "feszítő vas", isMooveable: false, feszítovasFunctions));

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

                            break;
                    }
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
            bathroomThings.Add(new Thing(position: "kelet", name: "kád", isMooveable: false, kádFunctions));
            bathroomThings.Add(new Thing(position: "kád", name: "feszítő vas", isMooveable: false, feszítovasFunctions));

            switch (interactions.Command)
            {
                case "menj":
                    switch (interactions.Item1)
                    {
                        case "észak":
                            response = "északnak nem tudsz menni arr nincs kijárat";
                            break;

                        case "dél":
                            response = "délnek nem tudsz menni arra nincs kijárat";
                            break;
                        case "kelet":
                            response = "keletnek mész a nézd paranccsal feltérképezheted a terepet";
                            break;
                        case "nyugat":
                            response = "";
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



