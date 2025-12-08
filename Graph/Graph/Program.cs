using System;
using System.Collections.Generic;

namespace Graph
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] cities =
            {
                "Rīga", "Dalbe", "Ozolnieki", "Jelgava", "Eleja",
                "Dobele", "Tukums", "Saldus", "Liepāja"
            };

            int[,] graph =
            {
                // Rīga Dalbe Ozolnieki Jelgava Eleja Dobele Tukums Saldus Liepāja
                { -1, 10, -1, 45, -1, -1, -1, -1, 78 }, // Rīga
                { 12, -1, 5, -1, -1, -1, -1, -1, -1 }, // Dalbe
                { -1, 5, -1, 7, -1, -1, -1, -1, -1 }, // Ozolnieki
                { 48, -1, 6, -1, -1, 30, -1, -1, -1 }, // Jelgava
                { -1, -1, -1, 22, -1, -1, -1, -1, -1 }, // Eleja
                { -1, -1, -1, 31, -1, -1, 22, 35, -1 }, // Dobele
                { -1, -1, -1, -1, -1, 21, -1, 18, -1 }, // Tukums
                { -1, -1, -1, -1, -1, 33, 22, -1, 55 }, // Saldus
                { 83, -1, -1, -1, -1, -1, -1, -1, -1 }  // Liepāja
            };

            Console.WriteLine("Available cities:");
            for (int i = 0; i < cities.Length; i++)
                Console.WriteLine($"{i}. {cities[i]}");

            Console.Write("\nEnter start city number: ");
            int start = int.Parse(Console.ReadLine());

            Console.Write("Enter destination city number: ");
            int end = int.Parse(Console.ReadLine());

            Console.WriteLine("\nSearching route...");

            var result = FindRoute(graph, cities, start, end);

            if (result == null)
            {
                Console.WriteLine("ERROR: Route cannot be found.");
            }
            else
            {
                Console.WriteLine("Route found:");
                for (int i = 0; i < result.Path.Count - 1; i++)
                {
                    int a = result.Path[i];
                    int b = result.Path[i + 1];

                    Console.WriteLine($"{cities[a]} -> {cities[b]} : {graph[a, b]}");
                }

                Console.WriteLine($"Total cost = {result.TotalCost}");
            }

            Console.WriteLine("\nCheapest route using Dijkstra:");
            var cheapest = Dijkstra(graph, cities, start, end);
            if (cheapest != null)
            {
                Console.WriteLine($"Cheapest cost = {cheapest.TotalCost}");
                Console.WriteLine("Path: " + string.Join(" -> ", cheapest.Path.ConvertAll(i => cities[i])));
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // ---------------------------- ROUTE RESULT CLASS ----------------------------
        public class RouteResult
        {
            public List<int> Path { get; set; }
            public int TotalCost { get; set; }
        }

        // ---------------------------- DEPTH-FIRST SEARCH ----------------------------
        static RouteResult FindRoute(int[,] graph, string[] cities, int start, int end)
        {
            bool[] visited = new bool[cities.Length];
            List<int> path = new List<int>();

            bool found = DFS(start, end, graph, visited, path);

            if (!found)
                return null;

            int total = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                total += graph[path[i], path[i + 1]];
            }

            return new RouteResult { Path = path, TotalCost = total };
        }

        static bool DFS(int current, int target, int[,] graph, bool[] visited, List<int> path)
        {
            visited[current] = true;
            path.Add(current);

            if (current == target)
                return true;

            for (int next = 0; next < visited.Length; next++)
            {
                if (graph[current, next] != -1 && !visited[next])
                {
                    if (DFS(next, target, graph, visited, path))
                        return true;
                }
            }

            path.RemoveAt(path.Count - 1);
            return false;
        }

        // ---------------------------- DIJKSTRA FOR CHEAPEST ROUTE ----------------------------
        static RouteResult Dijkstra(int[,] graph, string[] cities, int start, int end)
        {
            int n = cities.Length;

            int[] dist = new int[n];
            int[] prev = new int[n];
            bool[] used = new bool[n];

            for (int i = 0; i < n; i++)
            {
                dist[i] = int.MaxValue;
                prev[i] = -1;
            }

            dist[start] = 0;

            for (int i = 0; i < n; i++)
            {
                int u = -1;
                int best = int.MaxValue;

                for (int j = 0; j < n; j++)
                {
                    if (!used[j] && dist[j] < best)
                    {
                        best = dist[j];
                        u = j;
                    }
                }

                if (u == -1) break;
                used[u] = true;

                for (int v = 0; v < n; v++)
                {
                    if (graph[u, v] != -1)
                    {
                        int newDist = dist[u] + graph[u, v];
                        if (newDist < dist[v])
                        {
                            dist[v] = newDist;
                            prev[v] = u;
                        }
                    }
                }
            }

            if (dist[end] == int.MaxValue)
                return null;

            List<int> path = new List<int>();
            for (int at = end; at != -1; at = prev[at])
                path.Add(at);

            path.Reverse();

            return new RouteResult { Path = path, TotalCost = dist[end] };
        }
    }
}
