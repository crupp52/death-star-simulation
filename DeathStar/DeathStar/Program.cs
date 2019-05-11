using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeathStar
{
    class Program
    {
        static Stopwatch sw;

        static void Main(string[] args)
        {
            sw = new Stopwatch();

            List<Munkas> munkasok = Munkas.GetMunkasok();

            Task[] munkasTask = new Task[munkasok.Count];

            sw.Start();

            for (int i = 0; i < munkasTask.Length; i++)
            {
                int j = i;
                munkasTask[j] = new Task(() => 
                {
                    while (!munkasok[j].Elkeszult)
                    {
                        munkasok[j].Lep();
                    }
                });

                munkasTask[j].Start();
            }

            List<Rohamosztagos> rohamosztagosok = new List<Rohamosztagos>()
            {
                new Rohamosztagos(),
                new Rohamosztagos(),
                new Rohamosztagos()
            };

            Task[] rohamosztagosTask = new Task[rohamosztagosok.Count];

            for (int i = 0; i < rohamosztagosTask.Length; i++)
            {
                int j = i;
                rohamosztagosTask[j] = new Task(() =>
                {
                    while (true)
                    {
                        rohamosztagosok[j].Felugyel((from e in munkasok
                                                     where e.Figyelik == false && e.Allapot < 100
                                                     orderby e.Allapot ascending
                                                     select e).FirstOrDefault());
                        Thread.Sleep(3000);
                        rohamosztagosok[j].FelugyeletVege();
                    }
                });

                rohamosztagosTask[j].Start();
            }

            Task.Run(() =>
            {
                while (true)
                {
                    Log(munkasok, rohamosztagosok);
                    Thread.Sleep(300);
                }
            });

            Task.WaitAll(munkasTask);
            sw.Stop();
        }

        static void Log(List<Munkas> munkasok, List<Rohamosztagos> rohamosztagosok)
        {
            Console.Clear();
            Console.WriteLine("MUNKÁSOK");
            foreach (var item in munkasok)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("ROHAMOSZTAGOSOK");
            foreach (var item in rohamosztagosok)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Eltelt idő: {0}s", sw.ElapsedMilliseconds / 1000);
        }
    }
}
