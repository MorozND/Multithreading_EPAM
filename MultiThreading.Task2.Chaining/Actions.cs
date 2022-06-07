using System;
using System.Collections.Generic;

namespace MultiThreading.Task2.Chaining
{
    public static class Actions
    {
        static readonly Random _random;

        static Actions()
        {
            _random = new Random();
        }

        public static int[] GetRandomNumbers(int amount)
        {
            var result = new int[amount];

            for (int i = 0; i < amount; i++)
            {
                result[i] = _random.Next();
            }

            return result;
        }

        public static int[] MultiplyWithRandom(int[] inputArr, out int multiplicator)
        {
            multiplicator = _random.Next();

            for (int i = 0; i < inputArr.Length; i++)
            {
                inputArr[i] *= multiplicator;
            }

            return inputArr;
        }

        public static string GetArrayString(IEnumerable<int> arr)
        {
            return string.Concat("[ ", string.Join(", ", arr), " ]");
        }
    }
}
