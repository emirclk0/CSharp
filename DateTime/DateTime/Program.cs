using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select task:");
            Console.WriteLine("1 - TimeSpan Task");
            Console.WriteLine("2 - Calendar Task");
            Console.Write("Choose: ");
            int choice = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (choice)
            {
                case 1:
                    TimeSpanTask.Run();
                    break;

                case 2:
                    CalendarTask.Run();
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine("\nProgram finished.");
            Console.ReadKey();
        }
    }

    
    class TimeSpanTask
    {
        public static void Run()
        {
            Console.Write("Enter array size: ");
            int size = int.Parse(Console.ReadLine());

            TimeSpan[] arr = new TimeSpan[size];

            for (int i = 0; i < size; i++)
            {
                Console.Write($"Initialize array {i + 1} element: ");
                long days = long.Parse(Console.ReadLine());
                arr[i] = TimeSpan.FromDays(days);
            }

            Console.WriteLine("\nResult:");

            foreach (var ts in arr)
            {
                Print(ts);
                Console.WriteLine("--------------------------------------");
            }

            Console.WriteLine("Difference between the first and the last array element");
            TimeSpan diff = arr[0] - arr[size - 1];
            Print(diff);
        }

        public static void Print(TimeSpan t)
        {
            Console.WriteLine($"TimeSpan ({t.TotalDays}.00:00:00)");
            Console.WriteLine($"TotalDays: {t.TotalDays}");
            Console.WriteLine($"TotalHours: {t.TotalHours}");
            Console.WriteLine($"TotalMinutes: {t.TotalMinutes}");
            Console.WriteLine($"TotalSeconds: {t.TotalSeconds}");
            Console.WriteLine($"TotalMilliseconds: {t.TotalMilliseconds}");
            Console.WriteLine($"Ticks: {t.Ticks}");
        }
    }


    class CalendarTask
    {
        public static void Run()
        {
            GregorianCalendar myCal = new GregorianCalendar();

            System.DateTime[] dates = new System.DateTime[10];


            for (int i = 0; i < 10; i++)
            {
                Console.Write($"Enter a date for array {i + 1} element: ");

                string input = Console.ReadLine();

                string[] formats =
                {
                    "dd.MM.yyyy", "MM/dd/yyyy",
                    "dd-MM-yyyy", "dd/MM/yyyy",
                    "MM-dd-yyyy", "yyyy-MM-dd"
                };

                dates[i] = System.DateTime.ParseExact(
                    input,
                    formats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None
                );
            }

            Console.WriteLine("\nResult:\n");

            foreach (var dt in dates)
            {
                DisplayValues(myCal, dt);
                Console.WriteLine("---------------------------------------------");
            }
        }

        public static void DisplayValues(Calendar myCal, System.DateTime myDT)
        {
            Console.WriteLine($"Era: {myCal.GetEra(myDT)}");
            Console.WriteLine($"Year: {myCal.GetYear(myDT)}");
            Console.WriteLine($"Month:{myCal.GetMonth(myDT)}");
            Console.WriteLine($"DayOfYear: {myCal.GetDayOfYear(myDT)}");
            Console.WriteLine($"DayOfMonth:{myCal.GetDayOfMonth(myDT)}");
            Console.WriteLine($"DayOfWeek: {myCal.GetDayOfWeek(myDT)}");
        }
    }
}
