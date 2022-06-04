/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        const int MaxThreadsCount = 10;

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // feel free to add your code

            Console.ReadLine();
        }

        static Thread CreateThread(int value)
        {
            if (value == 1)
            {
                return new Thread(DecrementAction);
            }
        }

        static void DecrementAction(object value)
        {
            var intValue = (int)value;

            var decrementedValue = --intValue; ;

            Console.WriteLine($"Initial value = {intValue}. New Value = {decrementedValue}");

            value = decrementedValue;

            if (decrementedValue != 0)
            {

            }
        }
    }
}
