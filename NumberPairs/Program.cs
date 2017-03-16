using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace NumberPairs
{
    class Program
    {
        

        static void Main(string[] args)
        {
            // Инициализация
            ICollection<int> numbers = new Collection<int>();
            var rnd = new Random();
            for (int i = 0; i < 30; i++)
            {
                var number = rnd.Next(-100, 100);
                Console.Write(number + " ");
                numbers.Add(number);

            }
            int x = new Random().Next(0,50);
            Console.WriteLine($"X = {x}");

            Console.WriteLine("Пары значений");
            //Вывод пар
            PrintPairs(numbers, x);
            Console.ReadKey();
        }

        private static void PrintPairs(ICollection<int> numbers, int x)
        {
            //Поиск пар
            var tempList = new Collection<int>();
            do
            {
                var first = numbers.ElementAt(0);
                var added = false;
                int second = 0;
                foreach (var number in numbers)
                {
                    if (added && second == number) continue;
                    if (!added && first + number == x)
                    {
                        second = number;
                        Console.WriteLine($"{first} {second}");
                        added = true;
                    }
                    if (first == number || second == number) continue;
                    tempList.Add(number);
                }
                numbers = tempList;
                tempList = new Collection<int>();
            } while (numbers.Count > 1);
        }


    }
}
