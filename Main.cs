using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        AdjacencyListGraph graph = new AdjacencyListGraph();


        graph.AddEdge(0, 1);
        graph.AddEdge(1, 321);
        graph.AddEdge(321, 451);
        graph.AddEdge(2, 0);
        graph.AddVertex(5);
        graph.AddVertex(521);

        graph.PrintGraph();
        graph.connectivityCheck();
        graph.Distance(2,451);
        Console.WriteLine();


        graph.RemoveVertex(4);  
        graph.RemoveEdge(1, 0);
        graph.RemoveEdge(0, 2);
        graph.PrintGraph();
        graph.connectivityCheck();
        Console.WriteLine("\n///////////////////////////////////////\n");

        ///////////////////////////////

        AdjacencyMatrixGraph graph1 = new AdjacencyMatrixGraph(4);
        graph1.AddEdge(0, 1);
        graph1.AddEdge(0, 2);
        graph1.AddEdge(3, 1);
        graph1.AddVertex();
        graph1.AddVertex();
 
        graph1.PrintGraph();
        graph1.connectivityCheck();
        Console.WriteLine();
        graph1.RemoveEdge(0, 2);
//        graph1.RemoveEdge(2, 0);
        graph1.RemoveVertex(2);
        graph1.RemoveVertex(3);
        graph1.PrintGraph();
        graph1.connectivityCheck();
    }
}