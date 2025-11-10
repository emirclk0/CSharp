using System;
using System.Collections.Generic;

namespace Tree
{
    
    public class District
    {
        public string Name { get; set; }
        public int Area { get; set; }

        public District(string name, int area)
        {
            Name = name;
            Area = area;
        }

        public override string ToString()
        {
            return $"{Name}({Area})";
        }
    }

    
    public class Branch
    {
        public District district;
        public List<Branch> branches;

        public Branch(District district)
        {
            this.district = district;
            branches = new List<Branch>();
        }

        
        public void Print(string indent)
        {
            Console.WriteLine(indent + district.ToString());
            foreach (var subBranch in branches)
            {
                subBranch.Print(indent + "-------");
            }
        }

        
        public Branch Search(string name)
        {
            if (district.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                return this;

            foreach (var subBranch in branches)
            {
                var found = subBranch.Search(name);
                if (found != null)
                    return found;
            }
            return null;
        }
    }

    public class Tree
    {
        public List<Branch> branches;

        public Tree()
        {
            branches = new List<Branch>();
        }

        public void Print()
        {
            foreach (var branch in branches)
            {
                branch.Print("");
            }
        }

        public Branch Search(string name)
        {
            foreach (var branch in branches)
            {
                var found = branch.Search(name);
                if (found != null)
                    return found;
            }
            return null;
        }
    }

    
    class Program
    {
        static void Main()
        {
            Tree tree = new Tree();
            int choice;

            do
            {
                Console.WriteLine("\nActions:");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Add branch");
                Console.WriteLine("2 - Add sub branch");
                Console.WriteLine("3 - Edit district data");
                Console.WriteLine("4 - Print tree");
                Console.Write("? > ");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;

                    case 1:
                        Console.Write("Enter district name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter district area: ");
                        int area = int.Parse(Console.ReadLine());
                        tree.branches.Add(new Branch(new District(name, area)));
                        break;

                    case 2:
                        Console.Write("Enter the district name to add a new subdistrict: ");
                        string parentName = Console.ReadLine();
                        var parentBranch = tree.Search(parentName);
                        if (parentBranch == null)
                        {
                            Console.WriteLine("District not found!");
                            break;
                        }
                        Console.Write("Enter subdistrict name: ");
                        string subName = Console.ReadLine();
                        Console.Write("Enter subdistrict area: ");
                        int subArea = int.Parse(Console.ReadLine());
                        parentBranch.branches.Add(new Branch(new District(subName, subArea)));
                        break;

                    case 3:
                        Console.Write("Enter the name of the district to be edited: ");
                        string editName = Console.ReadLine();
                        var editBranch = tree.Search(editName);
                        if (editBranch == null)
                        {
                            Console.WriteLine("District not found!");
                            break;
                        }
                        Console.Write("Enter new district name: ");
                        string newName = Console.ReadLine();
                        Console.Write("Enter new district area: ");
                        int newArea = int.Parse(Console.ReadLine());
                        editBranch.district.Name = newName;
                        editBranch.district.Area = newArea;
                        break;

                    case 4:
                        tree.Print();
                        break;

                    default:
                        Console.WriteLine("Unknown action!");
                        break;
                }

            } while (choice != 0);
        }
    }
}
