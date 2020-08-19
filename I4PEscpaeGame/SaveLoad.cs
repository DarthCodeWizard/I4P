using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace I4PEscpaeGame
{
    class SaveLoad
    {
        public static void Save(List<string> Invertory, List<Thing> LivingRoomThings, List<Thing> BathroomThings, UserInteractions interactions)
        {
            string Filename = interactions.Item1;
            try
            {
                using (StreamWriter writer = new StreamWriter(Filename))
                {
                    foreach (var thing in LivingRoomThings)
                    {
                        writer.WriteLine(thing.Name + " " + thing.IsOpen + " " + thing.IsChecked + " " + "nappali");

                    }
                    foreach (var thing in BathroomThings)
                    {
                        writer.WriteLine(thing.Name + " " + thing.IsOpen + " " + thing.IsChecked + " " + "Fürdő");
                    }
                    foreach (var item in Invertory)
                    {
                        writer.WriteLine("Invertory" + " " + item);
                    }
                }
                interactions.Response = "Sikeres mentés";
            }
            
            catch (Exception e)
            {

                interactions.Response=e.Message.ToString();
            }
        }


        public static void Load(List<Thing> LivingThings, List<Thing> BathThings, List<string> Invent, UserInteractions interactions)
        {

            List<LoadedData> LivingThingsL = new List<LoadedData>();
            List<LoadedData> BathThingsL = new List<LoadedData>();
            List<string> InvL = new List<string>();
            // DataRetun Recive = new DataRetun(bath: BathThings, living: LivingThings, inv: Invent);
            string Filename = interactions.Item1;
            try
            {
                if (File.Exists(Filename) == true)
                {
                    string[] content = File.ReadAllLines(Filename.ToString());
                    char[] splitting = { ' ', ';' };

                    foreach (var sor in content)
                    {
                        string[] temp = sor.Split(splitting);

                        if (sor != null)
                        {
                            if (temp.Length == 4 && temp[3] == "nappali")
                            {
                                LivingThingsL.Add(new LoadedData(temp[0], bool.Parse(temp[1]), bool.Parse(temp[2])));
                            }
                            else if (temp.Length == 4 && temp[3] == "Fürdő")
                            {
                                BathThingsL.Add(new LoadedData(temp[0], bool.Parse(temp[1]), bool.Parse(temp[2])));
                            
                            }
                            else if (temp.Length==2 && temp[0] == "Invertory")
                            {
                                InvL.Add(temp[1]);

                            }
                            
                        }
                    }
                    if (!(LivingThingsL.Count == 0 || BathThingsL.Count == 0))
                    {
                        foreach (var thing in LivingThings)
                        {
                            foreach (var thingL in LivingThingsL)
                            {
                                if (thing.Name == thingL.Name)
                                {
                                    thing.IsChecked = thingL.IsChecked;
                                    thing.IsOpen = thingL.IsOpen;
                                }
                            }
                        }

                        foreach (var thing in BathThings)
                        {
                            foreach (var thingL in BathThingsL)
                            {
                                if (thing.Name == thingL.Name)
                                {
                                    thing.IsChecked = thingL.IsChecked;
                                    thing.IsOpen = thingL.IsOpen;

                                }
                            }
                        }
                        if (InvL.Count!=0)
                        {
                            for (int i = 0; i < InvL.Count; i++)
                            {
                                if (Invent.Count != 0)
                                {
                                    for (int j = 0; j < Invent.Count; j++)
                                
                                   
                                        if (!Invent.Contains(InvL[i]))
                                        {
                                            Invent.Add(InvL[i]);
                                        }
                      
                                        else if (!InvL.Contains(Invent[j]))
                                        {
                                            Invent.Remove(Invent[j]);
                                        }
                                }
                                else
                                {
                                    Invent.Add(InvL[i]);
                                }
                                    
                                
                            }

                        }
                            interactions.Response = "A(z)" + interactions.Item1 + "fájl betöltése sikeres volt.";
                    }

                }
                else if (!File.Exists(Filename))
                {
                    interactions.Response = "A megadott file nem létezik.";

                }

            }
            catch (Exception ex)
            {
                interactions.Response = ex.Message.ToString();
            }
            
        }
       
    }
}
