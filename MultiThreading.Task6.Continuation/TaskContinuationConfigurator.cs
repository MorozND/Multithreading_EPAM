using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    public static class TaskContinuationConfigurator
    {
        public static IEnumerable<Task> ConfigureAnyContinuation(Task task)
        {
            yield return task.ContinueWith(
                t => Console.WriteLine("Continuation for any result"),
                TaskContinuationOptions.None
            );
        }

        public static IEnumerable<Task> ConfigureFaultedContinuation(Task task)
        {
            yield return task.ContinueWith(
                t =>
                {
                    Console.WriteLine("Continuation on faulted");
                    Console.WriteLine($"Exception message: {t.Exception.Message}");
                },
                TaskContinuationOptions.OnlyOnFaulted
            );

            yield return task.ContinueWith(
                t =>
                {
                    Console.WriteLine("Continuation on faulted and on the antecedent's thread");
                    Console.WriteLine($"Exception message: {t.Exception.Message}");
                },
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously
            );
        }

        public static IEnumerable<Task> ConfigureCancelledContinuation(Task task)
        {
            yield return task.ContinueWith(
                t => Console.WriteLine("Continuation on canceled and outside of the thread pool"),
                TaskContinuationOptions.OnlyOnCanceled
            );
        }
    }
}
