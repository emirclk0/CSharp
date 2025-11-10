using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Bubble Sort Visualization ===");

        
        Console.Write("Enter array size: ");
        int size = Convert.ToInt32(Console.ReadLine());

        int[] numbers = new int[size];
        Random rand = new Random();

       
        Console.Write("Enter minimum random value: ");
        int minValue = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter maximum random value: ");
        int maxValue = Convert.ToInt32(Console.ReadLine());

        
        FillArray(numbers, rand, minValue, maxValue);
        Console.WriteLine("\nRandom array:");
        PrintArray(numbers);

        
        Console.Write("\nSort ascending or descending? (a/d): ");
        string order = Console.ReadLine().ToLower();

        if (order == "a")
        {
            BubbleSortAscending(numbers);
            Console.WriteLine("\nSorted array (Ascending):");
        }
        else if (order == "d")
        {
            BubbleSortDescending(numbers);
            Console.WriteLine("\nSorted array (Descending):");
        }
        else
        {
            Console.WriteLine("Invalid input, sorting ascending by default.");
            BubbleSortAscending(numbers);
            Console.WriteLine("\nSorted array (Ascending):");
        }

        PrintArray(numbers);
    }

    static void FillArray(int[] arr, Random rand, int min, int max)
    {
        for (int i = 0; i < arr.Length; i++)
            arr[i] = rand.Next(min, max + 1); 
    }

    static void PrintArray(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine($"{arr[i]} = {new string('+', arr[i])}");
        }
    }

    static void BubbleSortAscending(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - 1 - i; j++)
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
    }

    static void BubbleSortDescending(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - 1 - i; j++)
                if (arr[j] < arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
    }
}
