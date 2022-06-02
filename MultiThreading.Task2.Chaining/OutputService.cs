using System;

namespace MultiThreading.Task2.Chaining
{
    public interface IOutputService
    {
        void OutputTaskResult(int taskNumber, string description, string resultString);
    }

    public class ConsoleOutputService : IOutputService
    {
        public void OutputTaskResult(int taskNumber, string description, string resultString)
        {
            Console.WriteLine($"Task #{taskNumber} - {description}");
            Console.WriteLine(resultString);
            Console.WriteLine();
        }
    }
}
