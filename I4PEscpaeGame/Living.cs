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


            LivingRoomThings.Add(new Thing(breakable:false,isInSomething: false, container:"nappali", name: "ágy", isMooveable: false, ágyFunctions, isOpenable: false, isOpen: false));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething: false, container:"nappali",name: "szekrény", isMooveable: false, szekrényFunctions, isOpenable: true, isOpen: false));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething: false, container:"nappali",name: "ajtó", isMooveable: false, ajtoFunctions, isOpenable: true, isOpen: false));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething: true , container: "doboz", name: "kulcs", isMooveable: true, kulcsFunctions, isOpenable: false, isOpen: false));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething: true, container: "szekrény", name: "doboz", isMooveable: true, dobozFunctions, isOpenable: true, isOpen: false));
        }
        public static string LivingOnGame(UserInteractions interactions, List<string> Invertory)
        {
            string temp = "";
            List<Thing> LivingRoomThings = new List<Thing>();
            Init(LivingRoomThings);

            switch (interactions.Command)
            {
                case "nézd":
                    switch (interactions.Item1)
                    {
                        case"":
                            interactions.Response = "A nappaliban vagy. Itt található egy szekrény. Nyugatra látsz egy ajtót.";
                            break;
                        default:
                            foreach (var thing in LivingRoomThings)
                            {
                                if (interactions.Item1 == thing.Name)
                                {
                                    foreach (var func in thing.Functions)
                                    {
                                        if (interactions.Command == func.Key)
                                        {

                                            if (thing.IsInSomething)
                                            {
                                                foreach (var thing1 in LivingRoomThings)
                                                {
                                                    if (thing1.Name == thing.Container && thing1.IsOpen)
                                                    {
                                                        interactions.Response = func.Value;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                interactions.Response = func.Value;
                                            }
                                        }

                                    }
                                }
                                
                            }
                            if (interactions.Response=="")
                            {
                                interactions.Response = "a(z) " + interactions.Item1 + " tárgyat nem látom";
                            }
                            break;
                    }
                    break;


                case "menj":
                    switch (interactions.Item1)
                    {
                        case"észak":
                            break;
                        case "dél":
                            break;
                        case "kelet":
                            break;
                        case "nyugat":
                            break;
                        default:
                            break;
                    }

                    break;


                case "nyisd":
                    foreach (var thing in LivingRoomThings)
                    {
                        foreach (var func in thing.Functions)
                        {
                            if (thing.Name == interactions.Item1 && interactions.Command == func.Key && thing.IsOpenable == true)
                            {
                                interactions.Response = func.Value;
                                thing.IsOpen = true;
                            }
                           
                        }
                    }
                    if (interactions.Response == "")
                    {
                        interactions.Response = "a(z) " + interactions.Item1 + " tárgyat nem tudom kinyitni";
                    }
                    break;


                case"veddfel":
                    foreach (var thing in LivingRoomThings)
                    {
                        foreach (var func in thing.Functions)
                        {
                            if (thing.Name == interactions.Item1 && interactions.Command == func.Key && thing.IsMooveable)
                            {
                                interactions.Response = func.Value;
                                Invertory.Add(thing.Name);
                            }


                            
                        }
                    }
                    if (interactions.Response == "")
                    {
                        interactions.Response = "a(z) " + interactions.Item1 + " tárgyat nem tudom felvenni";
                    }
                    break;


                case"teddle":
                    foreach (var thing in LivingRoomThings)
                    {
                        foreach (var func in thing.Functions)
                        {
                            if (thing.Name == interactions.Item1 && interactions.Command == func.Key && Invertory.Contains(thing.Name))
                            {
                                interactions.Response = func.Value;
                                Invertory.Remove(thing.Name);
                            }



                        }
                    }
                    if (interactions.Response == "")
                    {
                        interactions.Response = "a(z) " + interactions.Item1 + " tárgya nincs a birtokodban";
                    }
                    break;


                case "húzd":
                    foreach (var thing in LivingRoomThings)
                    {
                        foreach (var func in thing.Functions)
                        {
                            if (thing.Name == interactions.Item1 && interactions.Command == func.Key && thing.IsMooveable)
                            {
                                interactions.Response = func.Value;
                            }


                           
                        }
                    }
                    if (interactions.Response == "")
                    {
                        interactions.Response = "a(z) " + interactions.Item1 + " tárgyat nem tudom elhúzni.";
                    }
                    break;


                case"törd":

                    break;



                default:
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
