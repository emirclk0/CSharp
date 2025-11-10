using System;
using System.Linq;

class Program
{
    static void Main()
    {
        const int size = 10;
        const int min = 1;
        const int max = 100;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Insertion Sort Program ===");
            Console.WriteLine("Choose array type to generate:");
            Console.WriteLine("1 - Ascending order");
            Console.WriteLine("2 - Random numbers");
            Console.WriteLine("3 - Unique random numbers");
            Console.WriteLine("4 - Nearly sorted (30% mixed)");
            Console.WriteLine("0 - Exit");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            if (choice == "0")
                break;

            int[] arr = null;

            switch (choice)
            {
                case "1":
                    arr = FillAscending(size, min);
                    Console.WriteLine("\n[Ascending order array created]");
                    break;
                case "2":
                    arr = FillRandom(size, min, max);
                    Console.WriteLine("\n[Random array created]");
                    break;
                case "3":
                    arr = FillUniqueRandom(size, min, max);
                    Console.WriteLine("\n[Unique random array created]");
                    break;
                case "4":
                    arr = FillNearlySorted(size, min, max);
                    Console.WriteLine("\n[Nearly sorted array created]");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    Console.ReadKey();
                    continue;
            }

            PrintArray("Before sorting", arr);

            InsertionSort(arr, out int operations);

            PrintArray("After sorting", arr);
            Console.WriteLine($"Number of operations: {operations}\n");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    
    static int[] FillAscending(int size, int start)
    {
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
            arr[i] = start + i;
        return arr;
    }

    
    static int[] FillRandom(int size, int min, int max)
    {
        Random rnd = new Random();
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
            arr[i] = rnd.Next(min, max + 1);
        return arr;
    }

    
    static int[] FillUniqueRandom(int size, int min, int max)
    {
        Random rnd = new Random();
        int[] pool = Enumerable.Range(min, max - min + 1).ToArray();
        return pool.OrderBy(x => rnd.Next()).Take(size).ToArray();
    }

    
    static int[] FillNearlySorted(int size, int min, int max)
    {
        int[] arr = FillAscending(size, min);
        Random rnd = new Random();
        int swaps = (int)(size * 0.3); 
        for (int i = 0; i < swaps; i++)
        {
            int index1 = rnd.Next(0, size);
            int index2 = rnd.Next(0, size);
            (arr[index1], arr[index2]) = (arr[index2], arr[index1]);
        }
        return arr;
    }

    
    static void InsertionSort(int[] arr, out int operations)
    {
        operations = 0;
        for (int i = 1; i < arr.Length; i++)
        {
            int key = arr[i];
            int j = i - 1;

            
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
                operations++;
            }
            arr[j + 1] = key;
            operations++;
        }
    }

    
    static void PrintArray(string title, int[] arr)
    {
        Console.WriteLine($"{title}: {string.Join(", ", arr)}");
    }
}
