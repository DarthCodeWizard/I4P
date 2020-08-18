
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
        public static List<string>Invertory = new List<string>();
        public static List<Thing> LivingRoomThings = new List<Thing>();
        public static List<Thing> BathroomThings = new List<Thing>();
        static void Main(string[] args)
        {
            try
            {

                UserInteractions interactions = new UserInteractions(response: "", command: "", item1: "", item2: "", room: "nappali", Invertory);
                Living.Init(LivingRoomThings);
                Bath.Init(BathroomThings);
               
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
                Console.WriteLine("A tovább lépéshez nyomj egy ENTER-t!");
                Console.ReadLine();
                Console.Clear();

                while (interactions.Response != "Betörted az ablakot.")
                {
                    read = Console.ReadLine().ToLower();
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


                    if (interactions.Command == "leltár")
                    {
                        if (interactions.Item1 == "" && interactions.Item2 == "")
                        {
                            if (Invertory.Count != 0)
                            {
                                Console.WriteLine("Nálad van :");
                                Console.WriteLine();
                                foreach (var item in Invertory)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nincs nálad semmi!");
                            }
                        }
                        else if (interactions.Response == "")
                        {
                            interactions.Response = "A leltár parancshoz nem kell megadj semmilyen paramétert";
                        }
                    }
                    if (interactions.Command == "mentés")
                    {
                        if (interactions.Item1 != "" && interactions.Item2 == "")
                        {
                            SaveLoad.Save(Invertory,LivingRoomThings,BathroomThings,interactions);
                        }
                        else if (interactions.Response == "")
                        {
                            interactions.Response = "A mentés parancshoz nem kell megadj semmilyen paramétert";
                        }
                    }
                    if (interactions.Command == "betöltés")
                    {
                        if (interactions.Item1 != "" && interactions.Item2 == "")
                        {
                            DataRetun RecivedData = SaveLoad.Load(LivingRoomThings, BathroomThings, Invertory,interactions);
                            LivingRoomThings = RecivedData.Living;
                            BathroomThings = RecivedData.Bath;
                            Invertory = RecivedData.Inv;
                        }
                        else if (interactions.Response == "")
                        {
                            interactions.Response = "A mentés parancshoz nem kell megadj semmilyen paramétert";
                        }
                    }

                    if (!(interactions.Command=="leltár" || interactions.Command == "mentés" || interactions.Command == "betöltés"))
                    {
                        switch (interactions.Room)
                        {
                            case "fürdő":
                                interactions.Response = Bath.BathOnGame(interactions, Invertory, BathroomThings);
                                break;
                            default:
                                interactions.Response = Living.LivingOnGame(interactions, Invertory, LivingRoomThings);
                                break;
                        }

                    }


                    Console.WriteLine(interactions.Response);
                    if (interactions.Response != "Betörted az ablakot.")
                    {
                        interactions.Response = "";
                        interactions.Command = "";
                        interactions.Item1 = "";
                        interactions.Item2 = "";

                    }
                    else
                    {
                        interactions.Command = "";
                        interactions.Item1 = "";
                        interactions.Item2 = "";
                    }
                    

                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Ki jutottál!");
                Console.WriteLine("A játék végetért!");
                Console.WriteLine("A kilépéshez nyom egy ENTER-t !");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex+" hiba lépett fel a játék futása közben!");
            }
           
        }
    } 
}



