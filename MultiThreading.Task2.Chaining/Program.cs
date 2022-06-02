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
            var taskActionFactory = new TaskActionFactory(new ConsoleOutputService());

            var taskNumber = 0;

            await Task.Run(() => taskActionFactory.RandomNumbersTask(++taskNumber, RandomAmount), cts.Token)
                .ContinueWith((t1) => taskActionFactory.MultiplicationTask(t1.Result, ++taskNumber), cts.Token)
                .ContinueWith((t2) => taskActionFactory.SortingTask(t2.Result, ++taskNumber), cts.Token)
                .ContinueWith((t3) => taskActionFactory.GetAverageTask(t3.Result, ++taskNumber), cts.Token);

            Console.ReadLine();
        }
    }
}
