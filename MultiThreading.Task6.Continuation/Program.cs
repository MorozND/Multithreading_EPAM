/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static readonly CancellationTokenSource _cts = new CancellationTokenSource();

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
            var taskRunOption = GetTaskRunOption(args);
            var taskFactory = new CustomTaskFactory();

            var task = taskFactory.CreateTaskByOption(taskRunOption, _cts.Token);

            var continuations = GetTaskContinuations(task);
            ProcessTask(task, taskRunOption);

            Task.WaitAny(continuations);

            Console.ReadLine();
        }

        static TaskRunOptions GetTaskRunOption(string[] args)
        {
            var optionArgument = args.FirstOrDefault();

            if (optionArgument is null)
                throw new ArgumentException("No TaskRunOption argument provided");

            if (!int.TryParse(optionArgument, out var intValue))
                throw new ArgumentException("Can't parse TaskRunOption user input");

            if (!Enum.IsDefined(typeof(TaskRunOptions), intValue))
                throw new ArgumentException("Invalid TaskRunOption provided");

            return (TaskRunOptions)intValue;
        }

        static Task[] GetTaskContinuations(Task task)
        {
            var result = new List<Task>();

            result.AddRange(
                TaskContinuationConfigurator.ConfigureAnyContinuation(task)
            );
            result.AddRange(
                TaskContinuationConfigurator.ConfigureFaultedContinuation(task)
            );
            result.AddRange(
                TaskContinuationConfigurator.ConfigureCancelledContinuation(task)
            );

            return result.ToArray();
        }

        static void ProcessTask(Task task, TaskRunOptions taskRunOption)
        {
            task.Start();

            if (taskRunOption == TaskRunOptions.Cancelled)
            {
                _cts.Cancel();
            }
        }
    }
}
