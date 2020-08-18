using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4PEscpaeGame
{
    class Bath
    {
        public static void Init(List<Thing> bathroomThings)
        {
         
            List<KeyValuePair<string, string>> kádFunctions = new List<KeyValuePair<string, string>>();
            kádFunctions.Add(new KeyValuePair<string, string>("nézd", "A kádban egy feszítővas van."));


            List<KeyValuePair<string, string>> feszítovasFunctions = new List<KeyValuePair<string, string>>();

            feszítovasFunctions.Add(new KeyValuePair<string, string>("nézd", "Ez egy feszítő vas"));
            feszítovasFunctions.Add(new KeyValuePair<string, string>("veddfel", "Felvetted a feszítővasat"));
         


            bathroomThings.Add(new Thing(breakable:false,isInSomething:false, container:"fürdő", name: "kád", isMooveable: false, kádFunctions, isOpenable: false,isOpen:false, isChecked:false, isPullable: false));
            bathroomThings.Add(new Thing(breakable: false, isInSomething: true, container: "kád", name: "feszítővas", isMooveable: false, feszítovasFunctions, isOpenable: false, isOpen: true, isChecked: false, isPullable: false));
        }
        public static string BathOnGame(UserInteractions interactions, List<string> Invertory,List<Thing> BathroomThings)
        {
            
            
          


            switch (interactions.Command)
            {
                case "nézd":
                    switch (interactions.Item1)
                    {
                        case "":
                            interactions.Response = "A fürdőben vagy. Délre található egy ajtó amin átjöttél a nappaliból, valamint Keletre látsz magad mellett egy kádat.";
                            break;
                        default:
                            foreach (var thing in BathroomThings)
                            {
                                if (interactions.Item1 == thing.Name)
                                {
                                    foreach (var func in thing.Functions)
                                    {
                                        if (interactions.Command == func.Key)
                                        {

                                            if (thing.IsInSomething)
                                            {
                                                foreach (var Container in BathroomThings)
                                                {
                                                    if (Container.Name == thing.Container && Container.IsChecked == true)
                                                    {
                                                        interactions.Response = func.Value;
                                                        thing.IsChecked = true;
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                interactions.Response = func.Value;
                                                thing.IsChecked = true;
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
                    }
                    break;


                case "menj":
                    switch (interactions.Item1)
                    {
                        case "észak":
                            interactions.Response = "Északra nem tudsz menni, arra nincs kijárat.";
                            break;
                        case "dél":
                            interactions.Response = "A nappaliban vagy";
                            interactions.Room = "nappali";
                            break;
                        case "kelet":
                            interactions.Response = "Eöltted van egy Kád.";
                            break;
                        case "nyugat":
                            interactions.Response = "Nyugatnak nem tudsz menni, arra nincs kijárat.";
                            break;
                        default:
                            break;
                    }

                    break;


                case "nyisd":
                    foreach (var thing in BathroomThings)
                    {
                        foreach (var func in thing.Functions)
                        {
                            if (thing.Name == interactions.Item1 && interactions.Command == func.Key && thing.IsOpenable == true && (interactions.Item1 != "ajtó" || interactions.Item2 != "ajtó"))
                            {
                                thing.IsOpen = true;
                                interactions.Response = func.Value;
                                break;
                            }

                        }
                    }
                    if (interactions.Response == "")
                    {
                        if (interactions.Item1 == "ajtó" && interactions.Item2 == "kulcs" && Invertory.Contains("kulcs") || interactions.Item1 == "kulcs" && interactions.Item2 == "ajtó" && Invertory.Contains("kulcs"))
                        {

                            interactions.Response = "Az ajtó nyitva van";
                        }
                        else
                        {
                            interactions.Response = "a(z) " + interactions.Item1 + " tárgyat nem tudom kinyitni";
                        }

                    }
                    break;


                case "veddfel":

                    foreach (var thing in BathroomThings)
                    {
                        foreach (var func in thing.Functions)
                        {
                            if (interactions.Command == func.Key)
                            {

                                if (thing.IsInSomething)
                                {
                                    foreach (var Container in BathroomThings)
                                    {
                                        if (Container.Name == thing.Container && Container.IsChecked == true)
                                        {
                                            interactions.Response = func.Value;
                                            Invertory.Add(thing.Name.ToString());
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (interactions.Response == "")
                    {
                        interactions.Response = "a(z) " + interactions.Item1 + " tárgyat nem tudom felvenni";
                    }
                    break;


                case "teddle":
                    foreach (var thing in BathroomThings)
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
                    foreach (var thing in BathroomThings)
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


                case "törd":
                    return interactions.Response = "Ez a parancs itt nem elérhető";
                  



                default:
                    if (interactions.Command == "")
                    {
                        interactions.Response = "Kérlek adj meg egy parancsot!";
                    }
                    else if (interactions.Response == "")
                    {
                        interactions.Response = "Ismeretlen parancs, kérlek próbálkozz egy érvényes paranccsal!";
                    }
                    break;
            }

            return interactions.Response;

        }
    }
}
