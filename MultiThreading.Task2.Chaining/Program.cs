/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        const int RandomAmount = 10;
        static readonly Random _random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            // feel free to add your code

            Console.ReadLine();
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

        static int[] Multiply(int[] inputArr)
        {
            var multiplicator = _random.Next();

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

        static void OuputTaskResult(int taskNumber, Action outputAction)
        {
            Console.WriteLine($"Task #{taskNumber}");
            outputAction();
        }

        static void OutputArray(int[] arr)
        {
            Console.WriteLine(string.Concat("[ ", string.Join(", ", arr), " ]"));
        }
    }
}
