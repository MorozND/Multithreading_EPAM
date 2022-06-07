using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    public class CustomTaskFactory
    {
        public Task CreateTaskByOption(TaskRunOptions option, CancellationToken cancellationToken)
        {
            if (option == TaskRunOptions.Failed)
            {
                return new Task(() =>
                    {
                        Console.WriteLine("Task started");

                        Console.WriteLine("Throwing manual exception");
                        throw new Exception("Manual exception");
                    },
                    cancellationToken
                );
            }
                

            return new Task(() =>
                {
                    Console.WriteLine("Regular task started");

                    cancellationToken.ThrowIfCancellationRequested();
                },
                cancellationToken
            );
        }
    }
}
