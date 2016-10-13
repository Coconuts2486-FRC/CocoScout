using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugSpace
{
    class Program
    {
        public enum List
        {
            A,
            B
        }
        static void Main(string[] args)
        {
            string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var reader = new StreamReader(File.OpenRead(docpath + @"\credentials.csv"));

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();

            List l = List.A;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                if(l == List.A)
                {
                    foreach(var x in values)
                    {
                        listA.Add(x);
                    }
                }
                else
                {
                    foreach (var x in values)
                    {
                        listB.Add(x);
                    }
                }

                if (l == List.A)
                    l = List.B;
            }
            Console.WriteLine("A");
            foreach (string i in listA)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("B");
            foreach (string i in listB)
            {
                Console.WriteLine(i);
            }
        }
    }
}
