using System;

namespace BinaryTree
{
    public class BNode
    {
        public int number;
        public BNode leftNode;
        public BNode rightNode;

        public BNode(int number)
        {
            this.number = number;
        }
    }

    public class BTree
    {
        public BNode rootNode;
        public int nodeNumber = 0;

        public void Add(int n)
        {
            if (rootNode == null)
            {
                rootNode = new BNode(n);
                nodeNumber++;
            }
            else
            {
                Add(n, rootNode);
            }
        }

        private void Add(int n, BNode parentNode)
        {
            if (n < parentNode.number)
            {
                if (parentNode.leftNode == null)
                {
                    parentNode.leftNode = new BNode(n);
                    nodeNumber++;
                }
                else Add(n, parentNode.leftNode);
            }
            else
            {
                if (parentNode.rightNode == null)
                {
                    parentNode.rightNode = new BNode(n);
                    nodeNumber++;
                }
                else Add(n, parentNode.rightNode);
            }
        }

        public void Print()
        {
            Print(rootNode, "");
        }

        private void Print(BNode N, string separator)
        {
            if (N == null) return;

            Console.WriteLine(separator + N.number);
            Print(N.leftNode, separator + "--");
            Print(N.rightNode, separator + "--");
        }

        public BNode FindNode(int n)
        {
            return FindNodeRecursive(rootNode, n);
        }

        private BNode FindNodeRecursive(BNode node, int n)
        {
            if (node == null) return null;
            if (node.number == n) return node;

            if (n < node.number)
                return FindNodeRecursive(node.leftNode, n);
            else
                return FindNodeRecursive(node.rightNode, n);
        }

        public void Remove(int number)
        {
            rootNode = RemoveRecursive(rootNode, number);
        }

        private BNode RemoveRecursive(BNode node, int number)
        {
            if (node == null) return null;

            if (number < node.number)
                node.leftNode = RemoveRecursive(node.leftNode, number);

            else if (number > node.number)
                node.rightNode = RemoveRecursive(node.rightNode, number);

            else
            {
                if (node.rightNode == null)
                    return node.leftNode;

                if (node.rightNode.leftNode == null)
                {
                    node.rightNode.leftNode = node.leftNode;
                    return node.rightNode;
                }

                BNode smallest = GetSmallest(node.rightNode);
                smallest.leftNode = node.leftNode;
                smallest.rightNode = node.rightNode;
                return smallest;
            }

            return node;
        }

        private BNode GetSmallest(BNode node)
        {
            BNode parent = node;
            BNode current = node.leftNode;

            while (current.leftNode != null)
            {
                parent = current;
                current = current.leftNode;
            }

            parent.leftNode = current.rightNode;
            return current;
        }

        public void Walk(BNode node)
        {
            if (node == null) return;

            nodeNumber++;
            Walk(node.leftNode);
            Walk(node.rightNode);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            BTree tree = new BTree();
            bool run = true;

            while (run)
            {
                Console.WriteLine("\n--- BINARY TREE MENU ---");
                Console.WriteLine("1 - Add Node");
                Console.WriteLine("2 - Remove Node");
                Console.WriteLine("3 - Print Tree");
                Console.WriteLine("4 - Find Node");
                Console.WriteLine("5 - Exit");
                Console.Write("Choose: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Enter number to add: ");
                        if (int.TryParse(Console.ReadLine(), out int addVal))
                        {
                            tree.Add(addVal);
                            Console.WriteLine("Added!");
                        }
                        else Console.WriteLine("Invalid number!");
                        break;

                    case "2":
                        Console.Write("Enter number to remove: ");
                        if (int.TryParse(Console.ReadLine(), out int removeVal))
                        {
                            tree.Remove(removeVal);
                            Console.WriteLine("Removed (if existed).");
                        }
                        else Console.WriteLine("Invalid number!");
                        break;

                    case "3":
                        Console.WriteLine("Tree Structure:");
                        tree.Print();
                        break;

                    case "4":
                        Console.Write("Enter number to find: ");
                        if (int.TryParse(Console.ReadLine(), out int findVal))
                        {
                            var node = tree.FindNode(findVal);
                            Console.WriteLine(node != null ? $"Found: {node.number}" : "Not found!");
                        }
                        else Console.WriteLine("Invalid number!");
                        break;

                    case "5":
                        run = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }

            Console.WriteLine("Program closed.");
        }
    }
}
