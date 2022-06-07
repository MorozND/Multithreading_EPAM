using System;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    public static class TaskContinuationConfigurator
    {
        public static void ConfigureAnyContinuation(Task task)
        {
            task.ContinueWith(
                t => Console.WriteLine("Continuation for any result"),
                TaskContinuationOptions.None
            );
        }

        public static void ConfigureFaultedContinuation(Task task)
        {
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
        }

        public static void ConfigureCancelledContinuation(Task task)
        {
            task.ContinueWith(
                t => Console.WriteLine("Continuation on canceled and outside of the thread pool"),
                TaskContinuationOptions.OnlyOnCanceled
            );
        }
    }
}
