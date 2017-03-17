using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace NumberPairs
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация
            Collection<int> numbers = new Collection<int>();
            var rnd = new Random(100);
            for (int i = 0; i < 1000000; i++)
            {
                var number = rnd.Next(int.MinValue, int.MaxValue);
//                Console.Write(number + " ");
                numbers.Add(number);

            }
            int x = new Random().Next(int.MinValue,int.MaxValue);
            Console.WriteLine($"X = {x}");

            //Вывод пар
            Console.WriteLine("Пары значений");
            var ts2 = Stopwatch.StartNew();
            ts2.Start();
            PrintPairsInSorted(numbers, x);
            ts2.Stop();
            Console.WriteLine($"Проход за {ts2.ElapsedMilliseconds} мс");
            Console.ReadKey();
        }

        private static void PrintPairsInSorted(Collection<int> numbers, int x)
        {
            if (numbers.Count <= 1) return;
            Sort(numbers, 0, numbers.Count - 1);
            var i = 0;
            var first = numbers.ElementAt(i);
            while (true)
            {
                if (i > numbers.Count - 2) break; // Если последний элемент коллекции, то выходим. Смысла искать нет.
                if ((x < 0 && first > 0 && int.MinValue - x <= first) || (x > 0 && first < 0 && x >= int.MaxValue + first)) // проверка на переполнение, try catch сильно тормозит
                {
                    i = GetNextIndexToProcess(numbers, i, first);
                    first = numbers.ElementAt(i);
                    continue;
                }
                var second = x - first;
                var index = SearchIndexInSorted(numbers, i+1, numbers.Count - 1, second);
                if (index >= 0)
                {
                    Console.WriteLine($"{first} {second}");
                }
                i = GetNextIndexToProcess(numbers, i, first);
                first = numbers.ElementAt(i);
            }
        }

        private static int GetNextIndexToProcess(Collection<int> numbers, int i, int first)
        {
            var numberIsProcessed = true;
            while (numberIsProcessed && i < numbers.Count - 1)
            {
                i++;
                if (first == numbers.ElementAt(i)) continue;
                numberIsProcessed = false;
            }
            return i;
        }

        private static int SearchIndexInSorted(Collection<int> collection, int start, int end, int x)
        {
            while (true)
            {
                if (end - start <= 1)
                {
                    if (collection.ElementAt(start) == x) return start;
                    if (collection.ElementAt(end) == x) return end;
                    return -1;
                }
                int index = start + (end-start)/2; // (end-start)/2 сделано для избежания переполнения
                var element = collection.ElementAt(index);
                if (element == x) return index;
                if (element < x)
                {
                    start = index + 1;
                    continue;
                }
                if (element > x)
                {
                    end = index - 1;
                    continue;
                }
                return -1;
            }
        }

        // Быстрая сортировка
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
