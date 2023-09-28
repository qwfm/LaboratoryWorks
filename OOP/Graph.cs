using System;
using System.Collections.Generic;

public interface IGraph
{
    void AddEdge(int vertex, int destination);
    void AddVertex(int ver);
    void RemoveEdge(int ver, int destination);
    void RemoveVertex(int ver);
    void PrintGraph();
    bool connectivityCheck();
    void Distance(int a, int b);
}


class AdjacencyListGraph : IGraph
{
    private Dictionary<int, List<int>> adjacencyList;

    public AdjacencyListGraph()
    {
        adjacencyList = new Dictionary<int, List<int>>();
    }

    public void AddEdge(int vertex, int destination)
    {
        AddVertex(vertex);
        AddVertex(destination);
        adjacencyList[vertex].Add(destination);
        adjacencyList[destination].Add(vertex);
    }
    public void AddVertex(int ver)
    {
        if (!adjacencyList.ContainsKey(ver))
        {
            adjacencyList[ver] = new List<int>();
        }
    }
    public void PrintGraph()
    {
        foreach (var kvp in adjacencyList)
        {
            Console.Write($"Вершина {kvp.Key}: ");
            foreach (var neighbor in kvp.Value)
            {
                Console.Write($"{neighbor} ");
            }
            Console.WriteLine();
        }
    }

    public void RemoveVertex(int ver)
    {
        if (adjacencyList.ContainsKey(ver))
        {
            foreach (var pair in adjacencyList[ver])
            {
                adjacencyList[pair].Remove(ver);
            }
            adjacencyList.Remove(ver);
        }
        else return;
    }

    public void RemoveEdge(int ver, int destination)
    {
        if (adjacencyList[ver].Contains(destination) && adjacencyList[destination].Contains(ver))
        {
            adjacencyList[ver].Remove(destination);
            adjacencyList[destination].Remove(ver);
        }
        else Console.WriteLine("Межі між двома вершинами не існує");
    }

    private void DFS(int vertex, HashSet<int> visited)
    {
        visited.Add(vertex);

        if (adjacencyList.ContainsKey(vertex))
        {
            foreach (var neighbor in adjacencyList[vertex])
            {
                if (!visited.Contains(neighbor))
                {
                    DFS(neighbor, visited);
                }
            }
        }
    }

    public bool connectivityCheck()
    {
        if (adjacencyList.Count == 0)
        {
            return true;
        }

        HashSet<int> visited = new HashSet<int>();
        int startVertex = adjacencyList.Keys.First();
        DFS(startVertex, visited);

        foreach (var vertex in adjacencyList.Keys)
        {
            if (!visited.Contains(vertex))
            {
                Console.WriteLine("Граф незв'язний!");
                return false;
            }
        }
        Console.WriteLine("Граф зв'язний!");
        return true;
    }

    public void Distance(int start, int end)
    {
        HashSet<int> visited = new HashSet<int>();
        Dictionary<int, int> distance = new Dictionary<int, int>();

        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);
        visited.Add(start);
        distance[start] = 0;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            foreach (int neighbor in adjacencyList[current])
            {
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    distance[neighbor] = distance[current] + 1;
                }
            }
        }

        if (distance.ContainsKey(end))
        {
            Console.WriteLine($"Відстань між вершиною {start} та {end} = {distance[end]}");
        }
        else
        {
            Console.WriteLine($"Вершина {end} не існує в графі.");
        }
    }
}

/// //////////////////////////////////////////////////////////////////// 


class AdjacencyMatrixGraph : IGraph
{
    private int[,] adjacencyMatrix;

    public AdjacencyMatrixGraph(int maxVertices)
    {
        adjacencyMatrix = new int[maxVertices, maxVertices];
    }

    public void AddEdge(int source, int destination)
    {
        if (source >= adjacencyMatrix.GetLength(0) || destination >= adjacencyMatrix.GetLength(0))
        {
            Console.WriteLine("Вершина виходить за границі матриці");
            return;
        }

        adjacencyMatrix[source, destination] = 1;
        adjacencyMatrix[destination, source] = 1;
    }

    public void AddVertex()
    {
        int size = adjacencyMatrix.GetLength(0);
        int newVerticesCount = size + 1;
        int[,] newMatrix = new int[newVerticesCount, newVerticesCount];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                newMatrix[i, j] = adjacencyMatrix[i, j];
            }
        }
        for (int i = 0; i < newVerticesCount; i++)
        {
            newMatrix[i, size] = 0;
            newMatrix[size, i] = 0;
        }
        adjacencyMatrix = newMatrix;
        size = newVerticesCount;
    }

    void IGraph.AddVertex(int ver)
    {
        AddVertex();
    }

    public void PrintGraph()
    {
        int numRows = adjacencyMatrix.GetLength(0);
        int numCols = adjacencyMatrix.GetLength(1);

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                Console.Write($"{adjacencyMatrix[i, j]} ");
            }
            Console.WriteLine();
        }
    }

    public void RemoveEdge(int ver, int dest)
    {
        if (adjacencyMatrix[ver, dest] == 1 || adjacencyMatrix[dest, ver] == 1)
        {
            adjacencyMatrix[ver, dest] = 0;
            adjacencyMatrix[dest, ver] = 0;
        }
        else Console.WriteLine("Вершини виходять за межі масиву");
    }

    public void RemoveVertex(int ver)
    {
        int numVertices = adjacencyMatrix.GetLength(0);
        if (ver < 0 || ver >= numVertices)
        {
            throw new ArgumentException("Неправильний ідекс вершини для видалення");
        }

        for (int i = 0; i < numVertices; i++)
        {
            adjacencyMatrix[ver, i] = 0;
        }

        for (int i = 0; i < numVertices; i++)
        {
            adjacencyMatrix[i, ver] = 0;
        }
        ver--;
    }

    private void DFS(int vertex, bool[] visited)
    {
        visited[vertex] = true;

        for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
        {
            if (adjacencyMatrix[vertex, i] == 1 && !visited[i])
            {
                DFS(i, visited);
            }
        }
    }

    public bool connectivityCheck()
    {
        if (adjacencyMatrix.GetLength(0) == 0)
        {
            return true;
        }

        int numVertices = adjacencyMatrix.GetLength(0);
        bool[] visited = new bool[numVertices];
        int startVertex = 0;
        DFS(startVertex, visited);

        foreach (var vertex in Enumerable.Range(0, numVertices))
        {
            if (!visited[vertex])
            {
                Console.WriteLine("Граф незв'язний!");
                return false;
            }
        }
        Console.WriteLine("Граф зв'язний!");
        return true;
    }

    public void Distance(int Start, int End)
    {

    }
}