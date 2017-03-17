using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace NumberPairs
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация
            Collection<int> numbers = new Collection<int>();
            var rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                var number = rnd.Next(-100, 100);
                Console.Write(number + " ");
                numbers.Add(number);

            }
            int x = new Random().Next(0,50);
            Console.WriteLine($"X = {x}");

            Console.WriteLine("Пары значений");
            //Вывод пар
            PrintPairsInSorted(numbers, x);
            Console.ReadKey();
        }

        private static void PrintPairsInSorted(Collection<int> numbers, int x)
        {
            if (numbers.Count <= 1) return;
            Sort(numbers, 0, numbers.Count - 1);
            var i = 0;
            while(true)
            {
                // Если последний элемент коллекции, то выходим. Смысла искать нет.
                if (i > numbers.Count - 2) break;
                var first = numbers.ElementAt(i);
                var second = x - first;
                var index = SearchIndexInSorted(numbers, i+1, numbers.Count - 1, second);
                if (index >= 0)
                {
                    Console.WriteLine($"{first} {second}");
                }
                i++;
            } 
        }

        private static int SearchIndexInSorted(Collection<int> collection, int start, int end, int x)
        {
            if (end == start || end-start == 1)
            {
                if (collection.ElementAt(start) == x) return start;
                if (collection.ElementAt(end) == x) return end;
                return -1;
            }
            int index = (end+start)/2;
            var element = collection.ElementAt(index);
            if (element == x) return index;
            if (element < x) return SearchIndexInSorted(collection, index + 1, end, x);
            if (element > x) return SearchIndexInSorted(collection, start, index - 1, x);
            return -1;
        }

        private static void Sort(Collection<int> collection, int l, int r)
        {
            if (collection.Count <= 1) return;
            int temp;
            int x = collection.ElementAt(l + (r - l) / 2);
            int i = l;
            int j = r;
            
            while (i <= j)
            {
                while (collection[i] < x) i++;
                while (collection[j] > x) j--;
                if (i <= j)
                {
                    temp = collection[i];
                    collection[i] = collection[j];
                    collection[j] = temp;
                    i++;
                    j--;
                }
            }
            if (i < r)
                Sort(collection, i, r);

            if (l < j)
                Sort(collection, l, j);
        }


    }
}
