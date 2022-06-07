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

        static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            PerformThreadJoinApproach();

            Console.WriteLine();

            PerformThreadPoolSemaphoreApproach();

            Console.ReadLine();
        }

        static void PerformThreadJoinApproach()
        {
            var threadState = new ThreadState
            {
                ThreadNumber = 1,
                Value = MaxThreadsCount
            };

            Console.WriteLine("Recursive threads approach:");
            var thread = RecursiveThread(threadState);
            thread.Join();
        }

        static void PerformThreadPoolSemaphoreApproach()
        {
            var threadState = new ThreadState
            {
                ThreadNumber = 1,
                Value = MaxThreadsCount
            };

            Console.WriteLine("Recursive ThreadPool with Semaphore approach:");
            RecursiveThreadPool(threadState);
        }

        static Thread RecursiveThread(object state)
        {
            var threadState = state as ThreadState;

            if (threadState is null)
                throw new ArgumentNullException(nameof(state));

            threadState.ThreadNumber++;

            if (threadState.ThreadNumber <= MaxThreadsCount)
            {
                var thread = RecursiveThread(threadState);
                thread.Join();
            }

            var newThread = new Thread(DecrementStateValue);
            newThread.Start(threadState);
            return newThread;
        }

        static void RecursiveThreadPool(object state)
        {
            var threadState = state as ThreadState;

            if (threadState is null)
                throw new ArgumentNullException(nameof(state));

            threadState.ThreadNumber++;

            if (threadState.ThreadNumber <= MaxThreadsCount)
            {
                RecursiveThreadPool(threadState);
            }

            ThreadPool.QueueUserWorkItem(DecrementStateValueWithSemaphoreLock, state);
        }

        static void DecrementStateValue(object state)
        {
            var threadState = state as ThreadState;

            if (threadState is null)
                throw new ArgumentNullException(nameof(state));

            var originalValue = threadState.Value;
            var decrementedValue = --threadState.Value;

            Console.WriteLine($"Initial value = {originalValue}. New Value = {decrementedValue}");
        }

        static void DecrementStateValueWithSemaphoreLock(object state)
        {
            _semaphoreSlim.Wait();

            DecrementStateValue(state);

            _semaphoreSlim.Release(1);
        }
    }
}
