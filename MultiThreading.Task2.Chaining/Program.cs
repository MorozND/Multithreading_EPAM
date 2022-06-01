/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        const int RandomAmount = 10;
        static readonly Random _random = new Random();

        static async Task Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            var cts = new CancellationTokenSource();

            var taskNumber = 0;

            await Task.Run(() => RandomNumbersTask(++taskNumber), cts.Token)
                .ContinueWith((t1) => MultiplicationTask(t1.Result, ++taskNumber), cts.Token)
                .ContinueWith((t2) => SortingTask(t2.Result, ++taskNumber), cts.Token)
                .ContinueWith((t3) => GetAverageTask(t3.Result, ++taskNumber), cts.Token);

            Console.ReadLine();
        }

        static int[] RandomNumbersTask(int taskNumber)
        {
            var result = GetRandomNumbers(RandomAmount);

            OutputTaskResult(taskNumber, "Creates an array of 10 random integer", GetArrayString(result));

            return result;
        }

        static int[] MultiplicationTask(int[] inputArr, int taskNumber)
        {
            var result = MultiplyWithRandom(inputArr, out int multiplicator);

            var outputString = string.Concat($"Multiplicator = {multiplicator}\n", GetArrayString(inputArr));

            OutputTaskResult(taskNumber, "Multiplies this array with another random integer", outputString);

            return result;
        }

        static int[] SortingTask(int[] inputArr, int taskNumber)
        {
            var result = SortByAscending(inputArr);

            OutputTaskResult(taskNumber, "Sorts this array by ascending",  GetArrayString(result));

            return result;
        }

        static int GetAverageTask(int[] inputArr, int taskNumber)
        {
            var result = GetAverage(inputArr);

            OutputTaskResult(taskNumber, "Calculates the average value", $"Average: {result}");

            return result;
        }

        static int[] GetRandomNumbers(int amount)
        {
            var result = new int[amount];

            for (int i = 0; i < amount; i++)
            {
                result[i] = _random.Next();
            }

            return result;
        }

        static int[] MultiplyWithRandom(int[] inputArr, out int multiplicator)
        {
            multiplicator = _random.Next();

            for (int i = 0; i < inputArr.Length; i++)
            {
                inputArr[i] *= multiplicator;
            }

            return inputArr;
        }

        static int[] SortByAscending(int[] inputArr)
        {
            return inputArr.OrderBy(x => x).ToArray();
        }

        static int GetAverage(int[] inputArr)
        {
            return (int)inputArr.Average();
        }

        static void OutputTaskResult(int taskNumber, string description, string resultString)
        {
            Console.WriteLine($"Task #{taskNumber} - {description}");
            Console.WriteLine(resultString);
            Console.WriteLine();
        }

        static string GetArrayString(int[] arr)
        {
            return string.Concat("[ ", string.Join(", ", arr), " ]");
        }
    }
}
