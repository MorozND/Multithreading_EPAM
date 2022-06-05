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
    class ThreadState
    {
        public int ThreadNumber { get; set; }
        public int Value { get; set; }
    }

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

            var threadState = new ThreadState
            {
                ThreadNumber = 1,
                Value = MaxThreadsCount
            };

            Console.WriteLine("Recursive threads approcah:");
            var thread = RecursiveThread(threadState);
            thread.Join();

            Console.ReadLine();
        }

        static Thread RecursiveThread(object state)
        {
            var threadState = state as ThreadState;

            threadState.ThreadNumber++;

            if (threadState.ThreadNumber < MaxThreadsCount)
            {
                var thread = RecursiveThread(threadState);
                thread.Join();
            }

            var newThread = new Thread(DecrementStateValue);
            newThread.Start(threadState);
            return newThread;
        }

        static void DecrementStateValue(object state)
        {
            var threadState = state as ThreadState;

            var originalValue = threadState.Value;
            var decrementedValue = threadState.Value - 1;

            Console.WriteLine($"Initial value = {originalValue}. New Value = {decrementedValue}");

            threadState.Value--;
        }
    }
}
