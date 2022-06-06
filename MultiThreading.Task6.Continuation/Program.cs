/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            // feel free to add your code

            var _cts = new CancellationTokenSource();
            var cancellationToken = _cts.Token;

            var taskRunOption = GetTaskRunOption(args[0]);

            var task = CreateTaskByOption(taskRunOption, cancellationToken);

            task.ContinueWith(
                t => Console.WriteLine("Continuation for any result"),
                TaskContinuationOptions.None
            );

            task.ContinueWith(
                t =>
                {
                    Console.WriteLine("Continuation on faulted");
                    Console.WriteLine($"Exception message: {t.Exception.Message}");
                },
                TaskContinuationOptions.OnlyOnFaulted
            );

            task.ContinueWith(
                t =>
                {
                    Console.WriteLine("Continuation on faulted and on the antecedent's thread");
                    Console.WriteLine($"Exception message: {t.Exception.Message}");
                },
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously
            );

            task.ContinueWith(
                t => Console.WriteLine("Continuation on canceled and outside of the thread pool"),
                TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning
            );

            if (taskRunOption == TaskRunOptions.Cancelled)
            {
                _cts.Cancel();
            }
            else
            {
                task.GetAwaiter().GetResult();
            }

            Console.ReadLine();
        }

        static TaskRunOptions GetTaskRunOption(string argument)
        {
            if (!int.TryParse(argument, out var intValue))
                throw new ArgumentException("Can't parse TaskRunOption user input");

            if (!Enum.IsDefined(typeof(TaskRunOptions), intValue))
                throw new ArgumentException("Invalid TaskRunOption provided");

            return (TaskRunOptions)intValue;
        }

        static Task CreateTaskByOption(TaskRunOptions option, CancellationToken cancellationToken)
        {
            if (option == TaskRunOptions.Failed)
                return Task.FromException(new Exception("Manual exception"));

            return Task.Run(() =>
                {
                    Console.WriteLine("Regular task started");

                    cancellationToken.ThrowIfCancellationRequested();
                }, 
                cancellationToken
            );
        }
    }
}
