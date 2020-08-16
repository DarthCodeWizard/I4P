using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4PEscpaeGame
{
    class Bath
    {
        private static void Init(List<Thing> bathroomThings)
        {
         
            List<KeyValuePair<string, string>> kádFunctions = new List<KeyValuePair<string, string>>();
            kádFunctions.Add(new KeyValuePair<string, string>("nézd", "a kádban egy feszítővas van"));


            List<KeyValuePair<string, string>> feszítovasFunctions = new List<KeyValuePair<string, string>>();

            feszítovasFunctions.Add(new KeyValuePair<string, string>("nézd", "ez egy feszítő vas"));
            feszítovasFunctions.Add(new KeyValuePair<string, string>("veddfel", "felvetted a feszítővasat"));
            feszítovasFunctions.Add(new KeyValuePair<string, string>("törd", "betörted a(z)"));


            bathroomThings.Add(new Thing(position: "kelet", name: "kád", isMooveable: false, kádFunctions, isOpenable: false, isOpen: false));
            bathroomThings.Add(new Thing(position: "kád", name: "feszítő vas", isMooveable: false, feszítovasFunctions, isOpenable: false, isOpen: false));
        }
        public static string BathOnGame(UserInteractions interactions, List<string> Invertory)
        {
            string temp = "";
            List<Thing> BathroomThings = new List<Thing>();
            Init(BathroomThings);

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
                    //átdolgozni a position propertyvel
                    foreach (var thing in BathroomThings)
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
