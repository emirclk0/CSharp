using System;

namespace DoublyLinkedList
{
    
    public class Stop
    {
        public int OrderNumber { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }

        
        public void Register()
        {
            Console.Write("Enter order number: ");
            OrderNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter stop name: ");
            Name = Console.ReadLine();

            Console.Write("Enter time: ");
            Time = Console.ReadLine();
        }

        
        public void Print()
        {
            Console.WriteLine($"Order: {OrderNumber}, Name: {Name}, Time: {Time}");
        }
    }

    
    public class Node
    {
        public Stop Data { get; set; }
        public Node Next { get; set; }
        public Node Prev { get; set; }

        public Node(Stop data)
        {
            Data = data;
        }
    }

    
    public class MyList
    {
        private Node head;
        private Node tail;

        
        public void Add(Stop p)
        {
            Node newNode = new Node(p);
            if (head == null)
            {
                head = tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
        }

        
        public void PrintAZ()
        {
            Console.WriteLine("\nList from first to last:");
            if (head == null)
            {
                Console.WriteLine("(empty)");
                return;
            }

            Node current = head;
            while (current != null)
            {
                current.Data.Print();
                current = current.Next;
            }
        }

        
        public void PrintZA()
        {
            Console.WriteLine("\nList from last to first:");
            if (tail == null)
            {
                Console.WriteLine("(empty)");
                return;
            }

            Node current = tail;
            while (current != null)
            {
                current.Data.Print();
                current = current.Prev;
            }
        }

        
        public void Insert(Stop p, int index)
        {
            if (index < 0 || index > Count())
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            Node newNode = new Node(p);

            if (index == 0)
            {
                newNode.Next = head;
                if (head != null)
                    head.Prev = newNode;
                head = newNode;
                if (tail == null)
                    tail = newNode;
                return;
            }

            Node current = head;
            for (int i = 0; i < index - 1; i++)
                current = current.Next;

            newNode.Next = current.Next;
            newNode.Prev = current;

            if (current.Next != null)
                current.Next.Prev = newNode;
            else
                tail = newNode;

            current.Next = newNode;
        }

        
        public int Count()
        {
            int count = 0;
            Node current = head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }

        
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count())
            {
                Console.WriteLine("Invalid index.");
                return;
            }

            if (index == 0)
            {
                head = head.Next;
                if (head != null)
                    head.Prev = null;
                else
                    tail = null;
                return;
            }

            Node current = head;
            for (int i = 0; i < index; i++)
                current = current.Next;

            if (current.Prev != null)
                current.Prev.Next = current.Next;
            if (current.Next != null)
                current.Next.Prev = current.Prev;
            else
                tail = current.Prev;
        }

        
        public Stop ElementAt(int index)
        {
            if (index < 0 || index >= Count())
                return null;

            Node current = head;
            for (int i = 0; i < index; i++)
                current = current.Next;
            return current.Data;
        }

        
        public void Clear()
        {
            head = null;
            tail = null;
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            MyList list = new MyList();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n==== DOUBLY LINKED LIST MENU ====");
                Console.WriteLine("1. Add new stop");
                Console.WriteLine("2. Insert stop at index");
                Console.WriteLine("3. Print list (A→Z)");
                Console.WriteLine("4. Print list (Z→A)");
                Console.WriteLine("5. Show stop at index");
                Console.WriteLine("6. Remove stop at index");
                Console.WriteLine("7. Show number of stops");
                Console.WriteLine("8. Clear list");
                Console.WriteLine("9. Exit");
                Console.Write("Select option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Stop s1 = new Stop();
                        s1.Register();
                        list.Add(s1);
                        Console.WriteLine("Stop added successfully!");
                        break;

                    case "2":
                        Console.Write("Enter index: ");
                        int idxInsert;
                        if (int.TryParse(Console.ReadLine(), out idxInsert))
                        {
                            Stop s2 = new Stop();
                            s2.Register();
                            list.Insert(s2, idxInsert);
                            Console.WriteLine("Stop inserted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid index input!");
                        }
                        break;

                    case "3":
                        list.PrintAZ();
                        break;

                    case "4":
                        list.PrintZA();
                        break;

                    case "5":
                        Console.Write("Enter index: ");
                        int idxElement;
                        if (int.TryParse(Console.ReadLine(), out idxElement))
                        {
                            Stop found = list.ElementAt(idxElement);
                            if (found != null)
                                found.Print();
                            else
                                Console.WriteLine("Index out of range.");
                        }
                        break;

                    case "6":
                        Console.Write("Enter index: ");
                        int idxRemove;
                        if (int.TryParse(Console.ReadLine(), out idxRemove))
                        {
                            list.RemoveAt(idxRemove);
                            Console.WriteLine("Stop removed if existed.");
                        }
                        break;

                    case "7":
                        Console.WriteLine($"Total stops in list: {list.Count()}");
                        break;

                    case "8":
                        list.Clear();
                        Console.WriteLine("List cleared.");
                        break;

                    case "9":
                        running = false;
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
