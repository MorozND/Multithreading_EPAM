using System.Linq;

namespace MultiThreading.Task2.Chaining
{
    public class TaskActionFactory
    {
        private readonly IOutputService _outputService;

        public TaskActionFactory(IOutputService outputService)
        {
            _outputService = outputService;
        }

        public int[] RandomNumbersTask(int taskNumber, int amount)
        {
            var result = Actions.GetRandomNumbers(amount);

            _outputService.OutputTaskResult(taskNumber, $"Creates an array of {amount} random integer", Actions.GetArrayString(result));

            return result;
        }

        public int[] MultiplicationTask(int[] inputArr, int taskNumber)
        {
            var result = Actions.MultiplyWithRandom(inputArr, out int multiplicator);

            var outputString = string.Concat($"Multiplicator = {multiplicator}\n", Actions.GetArrayString(inputArr));

            _outputService.OutputTaskResult(taskNumber, "Multiplies this array with another random integer", outputString);

            return result;
        }

        public int[] SortingTask(int[] inputArr, int taskNumber)
        {
            var result = inputArr.OrderBy(x => x).ToArray();

            _outputService.OutputTaskResult(taskNumber, "Sorts this array by ascending", Actions.GetArrayString(result));

            return result;
        }

        public int GetAverageTask(int[] inputArr, int taskNumber)
        {
            var result = (int)inputArr.Average();

            _outputService.OutputTaskResult(taskNumber, "Calculates the average value", $"Average: {result}");

            return result;
        }
    }
}
