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
            using (StreamWriter writer=new StreamWriter("mentes.sav"))
            {
                foreach (var thing in LivingRoomThings )
                {
                    writer.WriteLine(thing.Name + " " + thing.Container + " " +thing.IsOpenable + " "+ thing.IsOpen+" "+thing.IsMooveable+" "+thing.IsInSomething+" "+thing.Breakable+" "+thing.IsChecked+" "+thing.IsPulleable+" nappali ");
                    foreach (var function in thing.Functions)
                    {   
                        writer.WriteLine(function.Key + " " + function.Value);
                    }
                }
                foreach (var thing in BathroomThings)
                {
                    writer.WriteLine(thing.Name + " " + thing.Container + " " + thing.IsOpenable + " " + thing.IsOpen + " " + thing.IsMooveable + " " + thing.IsInSomething + " " + thing.Breakable + " " + thing.IsChecked + " " + thing.IsPulleable + " fürdő ");
                    foreach (var function in thing.Functions)
                    {
                        writer.WriteLine(function.Key + " " + function.Value);
                    }
                }
                foreach (var item in Invertory)
                {
                    writer.WriteLine("Invertory:"+item);
                }
            }
            interactions.Response="Sikeres mentés";
        }
        public static DataRetun Load(List<Thing>LivingThings,List<Thing>BathThings,List<string>inv)
        {
            string [] content = File.ReadAllLines("mentes.sav");
            char[] splitting = { ' ' };
            foreach (var sor in content)
            {
                string[] temp = sor.Split(splitting);
                
            }
            return new DataRetun { Bath = BathThings, Living = LivingThings, Inv = inv };
        }
       
    }
}
