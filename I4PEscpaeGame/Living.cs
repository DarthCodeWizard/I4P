using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace I4PEscpaeGame
{
    class Living
    {
        public static void Init(List<Thing>LivingRoomThings)
        {
            List<KeyValuePair<string, string>> ágyFunctions = new List<KeyValuePair<string, string>>();

            ágyFunctions.Add(new KeyValuePair<string, string>("nézd", " Ez egy ágy."));


            List<KeyValuePair<string, string>> szekrényFunctions = new List<KeyValuePair<string, string>>();

            szekrényFunctions.Add(new KeyValuePair<string, string>("nézd","Ez egy szekrény ami be van csukva."));
            szekrényFunctions.Add(new KeyValuePair<string, string>("nyisd","kinyitottad a szekrényt amiben egy doboz található."));
            szekrényFunctions.Add(new KeyValuePair<string, string>("húzd","elúztad a szekrényt és mögötte egy ablakot látsz."));


            List<KeyValuePair<string, string>> dobozFunctions = new List<KeyValuePair<string, string>>();

            dobozFunctions.Add(new KeyValuePair<string, string>("veddfel", "felvetted a dobozt"));
            dobozFunctions.Add(new KeyValuePair<string, string>("nyisd", "kinyitottad a dobozt és egy kulcs található benne"));
            dobozFunctions.Add(new KeyValuePair<string, string>("teddle", "le tetted a dobozt"));
            dobozFunctions.Add(new KeyValuePair<string, string>("nézd", "ez egy csukott doboz"));

            List<KeyValuePair<string, string>> ablakFuctions = new List<KeyValuePair<string, string>>();
            ablakFuctions.Add(new KeyValuePair<string, string>("nézd", "Ez az ablak úgytűnik be van szögelve."));
            ablakFuctions.Add(new KeyValuePair<string, string>("törd", "Betörted az ablakot."));


            List<KeyValuePair<string, string>> kulcsFunctions = new List<KeyValuePair<string, string>>();
            kulcsFunctions.Add(new KeyValuePair<string, string>("veddfel", "Felvetted a kulcsot."));
            kulcsFunctions.Add(new KeyValuePair<string, string>("teddle", "Le tetted a kulcsot."));
            kulcsFunctions.Add(new KeyValuePair<string, string>("nézd", "Ez egy kulcs valószínüleg egy ajtóhoz való."));

            List<KeyValuePair<string, string>> ajtoFunctions = new List<KeyValuePair<string, string>>();
            ajtoFunctions.Add(new KeyValuePair<string, string>("nyisd", "az ajtó kinyilt nyugatra a fürdőszoba van előtted"));


            LivingRoomThings.Add(new Thing(breakable:false,isInSomething:false,container:"nappali",name:"ágy",isMooveable: false, ágyFunctions, isOpenable: false,isOpen:false,isChecked:false,isPullable:false));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething:false,container:"nappali",name:"szekrény",isMooveable: false, szekrényFunctions, isOpenable: true, isOpen: false, isChecked:false, isPullable: true));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething:false,container:"nappali",name:"ajtó",isMooveable: false, ajtoFunctions, isOpenable: true, isOpen: false, isChecked: false, isPullable: false));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething:true ,container: "doboz",name:"kulcs",isMooveable: true, kulcsFunctions, isOpenable: false, isOpen: false, isChecked: false, isPullable: false));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething:true,container:"szekrény",name:"doboz",isMooveable: false, dobozFunctions, isOpenable: true, isOpen: false, isChecked: false, isPullable: false));
            LivingRoomThings.Add(new Thing(breakable: true, isInSomething: false, container: "szekrény", name: "ablak", isMooveable: false, ablakFuctions, isOpenable: false, isOpen: false, isChecked: false, isPullable: false));

        }
        public static string LivingOnGame(UserInteractions interactions, List<string> Invertory, List<Thing> LivingRoomThings)
        {
            switch (interactions.Command)
            {
                case "nézd":
                    switch (interactions.Item1)
                    {
                        case"":
                            interactions.Response = "A nappaliban vagy. Északra található egy szekrény és Keletre egy Ágy. Nyugatra látsz egy ajtót.";
                            break;
                        case "ablak":
                           
                                    foreach (var thing in LivingRoomThings)
                                    {
                                            foreach (var func in thing.Functions)
                                            {
                                                if (interactions.Command == func.Key)
                                                {
                                                        foreach (var Container in LivingRoomThings)
                                                        {
                                                            if (Container.Name == thing.Container && Container.IsChecked == true)
                                                            {
                                                                interactions.Response = func.Value;

                                                                break;
                                                            }
                                                        }
                                                }
                                           }
                                    }
                                    if (interactions.Response == "")
                                    {
                                        interactions.Response = "a(z) " + interactions.Item1 + " tárgyat nem látom";
                                    }



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
                                                foreach (var Container in LivingRoomThings)
                                                {
                                                    if (Container.Name == thing.Container && Container.IsOpen == true)
                                                    {
                                                        interactions.Response = func.Value;

                                                        break;
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
                            interactions.Response = "Elötted van egy szekrény.";
                            break;
                        case "dél":
                            interactions.Response = "Délnek nem tudsz menni arra nincs kijárat.";
                            break;
                        case "kelet":
                            interactions.Response = "Eöltted van egy ágy.";
                            break;
                        case "nyugat":
                            var ajtó = (from x in LivingRoomThings where (x.Name == "ajtó") select (x)).FirstOrDefault();
                            if (ajtó.IsOpen==true)
                            {
                                interactions.Response = "A fürdő szobában vagy itt található egy kád.";
                                interactions.Room = "fürdő";
                            }
                            else
                            {
                                interactions.Response = "Az utadat egy zárt ajtó állja.";
                            }
                           
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
                            if (thing.Name == interactions.Item1 && interactions.Command == func.Key && thing.IsOpenable == true && (interactions.Item1!="ajtó" || interactions.Item2 != "ajtó"))
                            {
                                thing.IsOpen = true;
                                interactions.Response = func.Value;
                                break;
                            }
                           
                        }
                    }
                    if (interactions.Response == "")
                    {
                        if ( interactions.Item1 == "ajtó" && interactions.Item2== "kulcs" && Invertory.Contains("kulcs")  ||  interactions.Item1=="kulcs" && interactions.Item2 == "ajtó" && Invertory.Contains("kulcs"))
                        {
                            foreach (var thing in LivingRoomThings)
                            {
                                foreach (var func in thing.Functions)
                                {
                                    if (interactions.Command == func.Key && thing.IsOpenable == true )
                                    {
                                        thing.IsOpen = true;
                                        interactions.Response = func.Value;
                                        break;

                                    }

                                }
                            }
                        }
                        if (interactions.Item1 == "ajtó" && interactions.Item2 == "kulcs" || interactions.Item2 == "kulcs" && interactions.Item1 == "ajtó")
                        {
                            interactions.Response = "az ajtó kinyitásához fel kell vegyél egy kulcsot.";
                        }
                        else
                        {
                            interactions.Response = "a(z) " + interactions.Item1 + " tárgyat nem tudom kinyitni";
                        }
                        
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
                                Invertory.Add(thing.Name.ToString());
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
                                break;
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
                            if (thing.Name == interactions.Item1 && interactions.Command == func.Key && thing.IsPulleable)
                            {
                                interactions.Response = func.Value;
                                thing.IsChecked = true;
                            }
                        }
                    }
                    if (interactions.Response == "")
                    {
                        interactions.Response = "a(z) " + interactions.Item1 + " tárgyat nem tudom elhúzni.";
                    }
                    break;


                case"törd":

                 
                        if (interactions.Item1 == "ablak" && interactions.Item2 == "feszítővas" && Invertory.Contains("feszítővas") || interactions.Item1 == "feszítővas" && interactions.Item2 == "ablak" && Invertory.Contains("feszítővas"))
                        {
                            foreach (var thing in LivingRoomThings)
                            {
                                foreach (var func in thing.Functions)
                                {
                                    foreach (var Container in LivingRoomThings)
                                    {
                                        if (interactions.Command == func.Key && thing.Breakable == true && Container.Name == thing.Container && Container.IsChecked)
                                        {

                                            interactions.Response = func.Value;
                                            interactions.Response = "Kijutottál!";
                                            break;

                                        }
                                    }
                                    

                                }
                            }
                        }
                        if (interactions.Item1 == "ablak" && interactions.Item2 == "feszítővas" || interactions.Item2 == "feszítővas" && interactions.Item1 == "ablak")
                        {
                            interactions.Response = "A feszítővas nincs a birtokodban.";
                        }
                        else if (interactions.Item1 == "ablak"|| interactions.Item2 == "ablak")
                        {
                            interactions.Response = "Az ablakot nem tudom kézzel betörni, mert megsérülök ";
                        }   
                        else
                        {
                            interactions.Response = "a(z) "+interactions.Item1+" tárgyat nem látom.";
                        }

                    
                    break;



                default:
                  
                    
                        interactions.Response = "Ismeretlen parancs, kérlek próbálkozz egy érvényes paranccsal!";
                    
                   
                    break;
            }

            return interactions.Response;
        }

    }
}
