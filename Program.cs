using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsolaParcial
{
    class Program
    {
        static void Main(string[] args)
        {           
            List<Node> nodos = new List<Node>();
            nodos.Add(new Node(1, -1));
            nodos.Add(new Node(2, -1));
            nodos.Add(new Node(3, -1));
            nodos.Add(new Node(5, -1));
            nodos.Add(new Node(1, -2));
            nodos.Add(new Node(4, -2));
            nodos.Add(new Node(5, -2));
            nodos.Add(new Node(1, -3));
            nodos.Add(new Node(2, -3));
            nodos.Add(new Node(4, -3));
            nodos.Add(new Node(5, -3));
            nodos.Add(new Node(1, -4));
            nodos.Add(new Node(2, -4));
            nodos.Add(new Node(3, -4));
            nodos.Add(new Node(4, -4));
            nodos.Add(new Node(1, -5));
            nodos.Add(new Node(3, -5));
            nodos.Add(new Node(4, -5));
            nodos.Add(new Node(5, -5));

            Stopwatch s = new Stopwatch();
            s.Start();

            SearchPath.EndingPoint = nodos[10];
            SearchPath.StartingPoint = nodos[0];
            SearchPath.PrintPath(SearchPath.Path(nodos));

            s.Stop();
            Console.WriteLine("El proceso se tomó" + s.ElapsedMilliseconds + " ms");
        }
    }
}
