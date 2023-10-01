using System;
using System.IO;
using System.Collections.Generic;
using System.Security.AccessControl;

class Program
{
    static void Main(string[] args)
    {
        string folderPath = @"C:\Users\User\Downloads\Prikol"; 
        FileSystem fileSystem = new FileSystem(folderPath);
        fileSystem.GetFileInfo();

        folderPath = @"C:\\";
        fileSystem = new FileSystem(folderPath);
        fileSystem.FindAndShow("Prikol", fileInfo => fileInfo.Name == "Prikol");
        fileSystem.FindAndShow("source-editor-image8.png", fileInfo => fileInfo.Name == "source-editor-image8.png" && fileInfo.LastWriteTime > DateTime.Now.AddDays(-1));
        
        AdjacencyListGraph graph = new AdjacencyListGraph();


        graph.AddEdge(0, 1);
        graph.AddEdge(1, 0);
        graph.AddEdge(1, 321);
        graph.AddEdge(321, 1);
        graph.AddEdge(321, 451);
        graph.AddEdge(451, 321);
        graph.AddEdge(451, 2);
        graph.AddEdge(2, 451);
        graph.AddEdge(2, 0);
        graph.AddEdge(0, 2);
        graph.AddVertex(5);
        graph.AddVertex(521);

        graph.PrintGraph();
        graph.weakConnectivityCheck();
        graph.Distance(2,451);
        Console.WriteLine();


        //graph.RemoveVertex(4);
        graph.RemoveVertex(321);   
        graph.RemoveEdge(1, 0);
        graph.RemoveEdge(0, 2);


        graph.PrintGraph();
        graph.weakConnectivityCheck();
        Console.WriteLine("\n///////////////////////////////////////\n");

        ///////////////////////////////

        AdjacencyMatrixGraph graph1 = new AdjacencyMatrixGraph(4);
        graph1.AddEdge(0, 1);
        graph1.AddEdge(0, 2);
        graph1.AddEdge(3, 1);
        graph1.AddEdge(1, 0);
        graph1.AddEdge(2, 0);
        graph1.AddEdge(1, 3);
        
        graph1.PrintGraph();
        graph1.weakConnectivityCheck();

        graph1.AddVertex();
        graph1.AddVertex();
        graph1.Distance(3,0);
        graph1.Distance(0,3);
        Console.WriteLine();
        graph1.RemoveEdge(0, 2);
        graph1.RemoveEdge(2, 0);
        graph1.RemoveVertex(2);
        graph1.RemoveVertex(3);
        graph1.PrintGraph();
        graph1.weakConnectivityCheck();
    }
}