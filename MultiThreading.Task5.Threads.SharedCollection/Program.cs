/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        static AutoResetEvent _writeEvent = new AutoResetEvent(true);
        static AutoResetEvent _readEvent = new AutoResetEvent(false);

        const int IterationsCount = 10;

        static readonly CancellationTokenSource _cts = new CancellationTokenSource();

        static List<int> _collection = new List<int>();

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            var readTask = Task.Run(ReadCollection, _cts.Token);
            var writeTask = Task.Run(WriteCollection, _cts.Token);

            Task.WaitAll(readTask, writeTask);

            _writeEvent.Close();
            _readEvent.Close();

            Console.ReadLine();
        }

        static void PrintCollection()
        {
            Console.WriteLine(string.Concat("[ ", string.Join(", ", _collection), " ]"));
        }

        static void ReadCollection()
        {
            for (int i = 0; i < IterationsCount; i++)
            {
                _readEvent.WaitOne();

                PrintCollection();

                _writeEvent.Set();
            }
        }

        static void WriteCollection()
        {
            for (int i = 0; i < IterationsCount; i++)
            {
                _writeEvent.WaitOne();

                _collection.Add(i + 1);

                _readEvent.Set();
            }
        }
    }
}
