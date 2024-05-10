using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        int t = int.Parse(Console.ReadLine()); // liczba grafów

        for (int i = 0; i < t; i++)
        {
            string type = Console.ReadLine(); // rodzaj grafu
            int n = int.Parse(Console.ReadLine()); // liczba krawędzi

            List<string> edges = new List<string>();

            for (int j = 0; j < n; j++)
            {
                string[] input = Console.ReadLine().Split();
                string edge = "";

                if (type == "d" || type == "dw") // dla grafu skierowanego
                {
                    edge = input[0] + " -> " + input[1];
                }
                else // dla grafu nieskierowanego
                {
                    edge = input[0] + " -- " + input[1];
                }

                if (type == "gw" || type == "dw") // jeśli graf jest ważony
                {
                    edge += " [label = " + input[2] + "]";
                }

                edges.Add(edge);
            }

            if (type == "d" || type == "dw") // dla grafu skierowanego
            {
                Console.WriteLine("digraph {");
            }
            else // dla grafu nieskierowanego
            {
                Console.WriteLine("graph {");
            }

            foreach (var edge in edges)
            {
                Console.WriteLine("\t" + edge + ";");
            }

            Console.WriteLine("}");
        }
    }
}
