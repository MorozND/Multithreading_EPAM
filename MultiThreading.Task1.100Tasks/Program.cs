﻿/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
    class Program
    {
        const int TaskAmount = 100;
        const int MaxIterationsCount = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. Multi threading V1.");
            Console.WriteLine("1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.");
            Console.WriteLine("Each Task should iterate from 1 to 1000 and print into the console the following string:");
            Console.WriteLine("“Task #0 – {iteration number}”.");
            Console.WriteLine();

            HundredTasks();

            Console.ReadLine();
        }

        static void HundredTasks()
        {
            // Provide CancellationToken support to be able to gracefully close tasks if requested
            var cts = new CancellationTokenSource();

            Task.WhenAll(GetTasks(cts.Token))
                .GetAwaiter()
                .GetResult();
        }

        static IEnumerable<Task> GetTasks(CancellationToken cancellationToken)
        {
            for (int i = 0; i < TaskAmount; i++)
            {
                var taskNumber = i;
                yield return Task.Run(() => TaskAction(taskNumber), cancellationToken);
            }
        }

        static void TaskAction(int taskNumber)
        {
            for (int i = 0; i < MaxIterationsCount; i++)
            {
                Output(taskNumber, i);
            }
        }

        static void Output(int taskNumber, int iterationNumber)
        {
            Console.WriteLine($"Task #{taskNumber} – {iterationNumber}");
        }
    }
}
