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
            szekrényFunctions.Add(new KeyValuePair<string, string>("nyisd","Kinyitottad a szekrényt amiben egy doboz található."));
            szekrényFunctions.Add(new KeyValuePair<string, string>("húzd","Elúztad a szekrényt és mögötte egy ablakot látsz."));


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
            ajtoFunctions.Add(new KeyValuePair<string, string>("nyisd", "Az ajtó kinyilt, nyugatra a fürdőszoba van előtted."));


            LivingRoomThings.Add(new Thing(breakable:false,isInSomething:false,container:"nappali",name:"ágy",isMooveable: false, ágyFunctions, isOpenable: false,isOpen:false,isChecked:false,isPullable:false));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething:false,container:"nappali",name:"szekrény",isMooveable: false, szekrényFunctions, isOpenable: true, isOpen: false, isChecked:false, isPullable: true));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething:false,container:"nappali",name:"ajtó",isMooveable: false, ajtoFunctions, isOpenable: true, isOpen: false, isChecked: false, isPullable: false));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething:true ,container: "doboz",name:"kulcs",isMooveable: true, kulcsFunctions, isOpenable: false, isOpen: false, isChecked: false, isPullable: false));
            LivingRoomThings.Add(new Thing(breakable:false,isInSomething:true,container:"szekrény",name:"doboz",isMooveable: true, dobozFunctions, isOpenable: true, isOpen: false, isChecked: false, isPullable: false));
            LivingRoomThings.Add(new Thing(breakable: true, isInSomething: false, container: "szekrény", name: "ablak", isMooveable: false, ablakFuctions, isOpenable: false, isOpen: false, isChecked: false, isPullable: false));

        }
        public static string LivingOnGame(UserInteractions interactions, List<string> Invertory, List<Thing> LivingRoomThings)
        {
            switch (interactions.Command)
            {
                case "nézd":
                    switch (interactions.Item1)
                    {
                        case "":
                            return interactions.Response = "A nappaliban vagy. Északra található egy szekrény és Keletre egy Ágy. Nyugatra látsz egy ajtót.";
                            


                        case "ablak":

                            foreach (var thing in LivingRoomThings)
                            {
                                foreach (var func in thing.Functions)
                                {
                                    if (interactions.Command == func.Key && thing.Name=="ablak")
                                    {
                                        foreach (var Container in LivingRoomThings)
                                        {
                                            if (Container.Name == thing.Container && Container.IsChecked == true)
                                            {
                                                return interactions.Response = func.Value;
                                            }
                                        }
                                    }
                                }
                            }
                            if (interactions.Response == "")
                            {
                                return interactions.Response = "A(z) " + interactions.Item1 + " tárgyat nem látom.";
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
                                                        return interactions.Response = func.Value;


                                                    }
                                                }
                                            }
                                            else
                                            {
                                                return interactions.Response = func.Value;

                                            }
                                        }

                                    }
                                }
                            }
                            if (interactions.Response == "")
                            {
                                return interactions.Response = "A(z) " + interactions.Item1 + " tárgyat nem látom";
                            }
                            break;
                    }
                    break;


                case "menj":
                    switch (interactions.Item1)
                    {
                        case "észak":
                            return interactions.Response = "Elötted van egy szekrény.";
                           
                        case "dél":
                            return interactions.Response = "Délnek nem tudsz menni arra nincs kijárat.";
                        
                        case "kelet":
                            return interactions.Response = "Eöltted van egy ágy.";
                         
                        case "nyugat":
                            var ajtó = (from x in LivingRoomThings where (x.Name == "ajtó") select (x)).FirstOrDefault();
                            if (ajtó.IsOpen == true)
                            {
                                interactions.Room = "fürdő";
                                return interactions.Response = "A fürdő szobában vagy itt található egy kád.";
                               
                            }
                            else
                            {
                                return interactions.Response = "Az utadat egy zárt ajtó állja.";
                            }
                            
                        default:
                            return interactions.Response = "Ismeretlen irány!";
                          
                    }
                 


                case "nyisd":

                    foreach (var thing in LivingRoomThings)
                    {
                        foreach (var func in thing.Functions)
                        {
                            if (interactions.Item1 == thing.Name && interactions.Command == func.Key && thing.IsOpenable == true && !(interactions.Item1 == "ajtó" || interactions.Item2 == "kulcs"))
                            {
                                thing.IsOpen = true;
                                return interactions.Response = func.Value;
                               
                            }

                        }
                    }
                    if (interactions.Response == "")
                    {
                        if (interactions.Item1 == "ajtó" && interactions.Item2 == "kulcs" && Invertory.Contains("kulcs"))
                        {
                            foreach (var thing in LivingRoomThings)
                            {
                                foreach (var func in thing.Functions)
                                {
                                    if (interactions.Command == func.Key && interactions.Item1 == thing.Name && thing.IsOpenable == true)
                                    {
                                        thing.IsOpen = true;
                                        return interactions.Response = func.Value;
                                        

                                    }
                                }
                            }
                        }
                        if (interactions.Response == "" && interactions.Item1 == "ajtó" && interactions.Item2 == "kulcs")
                        {
                            return interactions.Response = "Az ajtó kinyitásához fel kell vegyél egy kulcsot.";
                        }
                        else if (interactions.Response == "")
                        {
                            if (interactions.Item1 != "")
                            {
                                if (interactions.Item1 != "" && interactions.Item2 != "")
                                {
                                    return interactions.Response = "A(z) " + interactions.Item1 + " tárgyat a(z) " + interactions.Item2 + " tárggyal nem tudom kinyitni.";
                                }
                                else
                                {
                                    return interactions.Response = "A(z) " + interactions.Item1 + " tárgyat nem tudom kinyitni.";
                                }
                            }
                            else
                            {
                                return interactions.Response = "A parancs használatához addj meg egy tárgyat amit ki akarsz nyitni.";
                            }
                        }
                    }
                    break;


                case "veddfel":
                    foreach (var thing in LivingRoomThings)
                    {
                        foreach (var func in thing.Functions)
                        {
                            if (interactions.Item1==thing.Name && interactions.Command == func.Key && thing.IsMooveable)
                            {
                                Invertory.Add(thing.Name.ToString());
                                return interactions.Response = func.Value;
                            }
                        }
                    }
                    if (interactions.Response == "")
                    {
                        return interactions.Response = "A(z) " + interactions.Item1 + " tárgyat nem tudom felvenni.";
                    }
                    break;


                case "teddle":
                    foreach (var thing in LivingRoomThings)
                    {
                        foreach (var func in thing.Functions)
                        {
                            if (thing.Name == interactions.Item1 && interactions.Command == func.Key && Invertory.Contains(thing.Name))
                            {
                                Invertory.Remove(thing.Name);
                                return interactions.Response = func.Value;
                                
                            }
                        }
                    }
                    if (interactions.Response == "")
                    {
                        return interactions.Response = "A(z) " + interactions.Item1 + " tárgya nincs a birtokodban.";
                    }
                    break;


                case "húzd":
                    foreach (var thing in LivingRoomThings)
                    {
                        foreach (var func in thing.Functions)
                        {
                            if (thing.Name == interactions.Item1 && interactions.Command == func.Key && thing.IsPulleable)
                            {
                                thing.IsChecked = true;
                                return interactions.Response = func.Value;
                               
                            }
                        }
                    }
                    if (interactions.Response == "")
                    {
                        return interactions.Response = "A(z) " + interactions.Item1 + " tárgyat nem tudom elhúzni.";
                    }
                    break;


                case "törd":

                    if ((interactions.Item1 == "ablak" && interactions.Item2 == "feszítővas" && Invertory.Contains("feszítővas")) || (interactions.Item1 == "feszítővas" && interactions.Item2 == "ablak" && Invertory.Contains("feszítővas")))
                    {
                        foreach (var thing in LivingRoomThings)
                        {
                            foreach (var func in thing.Functions)
                            {
                                if (interactions.Command == func.Key && thing.Breakable == true)
                                {
                                    foreach (var Container in LivingRoomThings)
                                    {
                                        if (Container.Name == thing.Container && Container.IsChecked)
                                        {
                                            return interactions.Response = func.Value;
                                           
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (interactions.Response == "" && interactions.Item1 == "ablak" && interactions.Item2 == "feszítővas" && !Invertory.Contains("feszítővas") || interactions.Item2 == "feszítővas" && interactions.Item1 == "ablak" && !Invertory.Contains("feszítővas"))
                    {
                        return interactions.Response = "A feszítővas nincs a birtokodban.";
                    }
                    else if (interactions.Response == "" && interactions.Item1 == "ablak" || interactions.Item2 == "ablak")
                    { var szekrenyElvaneHuzva = (from x in LivingRoomThings where (x.Name == "szekrény") select (x)).FirstOrDefault();
                        if (szekrenyElvaneHuzva.IsChecked == true)
                        {
                            return interactions.Response = "Az ablakot nem tudom kézzel betörni, mert megsérülök ";
                        }
                        else if (interactions.Response == "")
                        {
                            return interactions.Response = "Az " + interactions.Item1 + " tárgyat nem találom az összetöréshez";
                        }
                    }
                    else if (interactions.Response == "")
                    {
                        return interactions.Response = "A(z) " + interactions.Item1 + " tárgyat nem tudom betörni.";
                    }
                    break;


                default:
                    if (interactions.Command == "")
                    {
                        return interactions.Response = "Kérlek adj meg egy parancsot!";
                    }
                    else if (interactions.Response=="") 
                    {
                        return interactions.Response = "Ismeretlen parancs, kérlek próbálkozz egy érvényes paranccsal!";
                    }  
                    break;
            }
            return interactions.Response = "Váratlan hiba lépett fel!";
        }
     }
}


