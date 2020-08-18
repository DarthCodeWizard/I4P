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
        public static void Save(List<string>Invertory,List<Thing>LivingRoomThings,List<Thing>BathroomThings,UserInteractions interactions)
        {
            string Filename = interactions.Item1;
            using (StreamWriter writer=new StreamWriter(Filename))
            {
                foreach (var thing in LivingRoomThings )
                {
                    writer.WriteLine(thing.Name + " " + thing.IsOpenable + " "+ thing.IsOpen+" "+thing.IsMooveable+" "+thing.IsInSomething+" "+thing.Breakable+" "+thing.IsChecked+" "+thing.IsPulleable+" nappali ");
                    
                }
                foreach (var thing in BathroomThings)
                {
                    writer.WriteLine(thing.Name + " " + thing.IsOpenable + " " + thing.IsOpen + " " + thing.IsMooveable + " " + thing.IsInSomething + " " + thing.Breakable + " " + thing.IsChecked + " " + thing.IsPulleable + " fürdő ");
                   
                }
                foreach (var item in Invertory)
                {
                    writer.WriteLine("Invertory:"+item);
                }
            }
            interactions.Response="Sikeres mentés";
        }
        public static DataRetun Load(List<Thing>LivingThings,List<Thing>BathThings,List<string>Invent, UserInteractions interactions)
        {
            List<LoadedData> LivingThingsL = new List<LoadedData>();
            List<LoadedData> BathThingsL = new List<LoadedData>();
            List<string> InvL = new List<string>();
            string Filename = interactions.Item1;
            if (File.Exists(Filename))
            {
                string[] content = File.ReadAllLines(Filename);
                char[] splitting = { ' ' };

                foreach (var sor in content)
                {
                    string[] temp = sor.Split(splitting);
                    if (temp[8] == "nappali")
                    {
                        LivingThingsL.Add(new LoadedData(temp[0], Convert.ToBoolean(temp[1]), Convert.ToBoolean(temp[2]), Convert.ToBoolean(temp[3]), Convert.ToBoolean(temp[4]), Convert.ToBoolean(temp[5]), Convert.ToBoolean(temp[6]), Convert.ToBoolean(temp[7])));
                    }
                    else if (temp[8] == "fürdő")
                    {
                        BathThingsL.Add(new LoadedData(temp[0], Convert.ToBoolean(temp[1]), Convert.ToBoolean(temp[2]), Convert.ToBoolean(temp[3]), Convert.ToBoolean(temp[4]), Convert.ToBoolean(temp[5]), Convert.ToBoolean(temp[6]), Convert.ToBoolean(temp[7])));
                    }
                    else
                    {
                        InvL.Add(temp[1]);
                    }
                }
                foreach (var thing in LivingThings)
                {
                    foreach (var thingL in LivingThingsL)
                    {
                        if (thing.Name == thingL.Name)
                        {
                            thing.IsChecked = thingL.IsChecked;
                            thing.IsInSomething = thingL.IsInSomething;
                            thing.IsMooveable = thingL.IsMooveable;
                            thing.IsOpen = thingL.IsOpen;
                            thing.IsOpenable = thingL.IsOpenable;
                            thing.IsPulleable = thingL.IsPulleable;
                            thing.Breakable = thingL.IsBreakable;

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
                            thing.IsInSomething = thingL.IsInSomething;
                            thing.IsMooveable = thingL.IsMooveable;
                            thing.IsOpen = thingL.IsOpen;
                            thing.IsOpenable = thingL.IsOpenable;
                            thing.IsPulleable = thingL.IsPulleable;
                            thing.Breakable = thingL.IsBreakable;

                        }
                    }
                }

                Invent = InvL;

                interactions.Response = "Betöltés Sikerült a" + interactions.Item1 + "fájlból";
            }
            else if (!File.Exists(Filename))
            {
                interactions.Response = "A megadott file nem létezik";
            }
            
           
            return new DataRetun { Bath = BathThings, Living = LivingThings, Inv = Invent };
           
        }
       
    }
}
