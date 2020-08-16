using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4PEscpaeGame
{
    class Living
    {
        private static void Init(List<Thing>LivingRoomThings)
        {
            List<KeyValuePair<string, string>> ágyFunctions = new List<KeyValuePair<string, string>>();

            ágyFunctions.Add(new KeyValuePair<string, string>("nézd", " Ez egy ágy."));


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
            kulcsFunctions.Add(new KeyValuePair<string, string>("veddfel", "Felvetted a kulcsot."));
            kulcsFunctions.Add(new KeyValuePair<string, string>("teddle", "Le tetted a kulcsot."));
            kulcsFunctions.Add(new KeyValuePair<string, string>("nézd", "Ez egy kulcs valószínüleg egy ajtóhoz való."));

            List<KeyValuePair<string, string>> ajtoFunctions = new List<KeyValuePair<string, string>>();
            ajtoFunctions.Add(new KeyValuePair<string, string>("nyisd", "az ajtó kinyilt nyugatra a fürdőszoba van előtted"));


            LivingRoomThings.Add(new Thing(position: "kelet", name: "ágy", isMooveable: false, ágyFunctions, isOpenable: false, isOpen: false));
            LivingRoomThings.Add(new Thing(position: "észak", name: "szekrény", isMooveable: false, szekrényFunctions, isOpenable: true, isOpen: false));
            LivingRoomThings.Add(new Thing(position: "nyugat", name: "ajtó", isMooveable: false, ajtoFunctions, isOpenable: true, isOpen: false));
            LivingRoomThings.Add(new Thing(position: "doboz", name: "kulcs", isMooveable: true, kulcsFunctions, isOpenable: false, isOpen: false));
            LivingRoomThings.Add(new Thing(position: "szekrény", name: "doboz", isMooveable: true, dobozFunctions, isOpenable: true, isOpen: false));
        }
        public static string LivingOnGame(UserInteractions interactions, List<string> Invertory)
        {
            string temp = "";
            List<Thing> LivingRoomThings = new List<Thing>();
            Init(LivingRoomThings);

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
                    //át kell dolgozni a position propretyvel
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

                                else if (thing.IsOpenable && interactions.Command == "nyisd" && interactions.Command == funct.Key)
                                {
                                    thing.IsOpen = true;
                                }

                                if (interactions.Command == funct.Key)
                                {
                                    temp = funct.Value.ToString();
                                    interactions.Response = temp;



                                    if (thing.IsMooveable && interactions.Command == "veddfel")
                                    {
                                        Invertory.Add(thing.Name);
                                    }
                                    else if (thing.IsMooveable && interactions.Command == "teddle")
                                    {
                                        Invertory.Remove(thing.Name);

                                    }

                                }

                            }
                            if (temp == "")
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
            if (interactions.Command == "nézd" && interactions.Item1 == "" && interactions.Item2 == "")
            {
                interactions.Response = "A nappaliban vagy itt található egy szekrény. Nyugatra látsz egy ajtót. ";
            }
            return interactions.Response;
        }

    }
}
