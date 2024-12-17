using Factorial.API.Infrastructure;

namespace Factorial.API.Services.Factorial;

public class NumberService : INumberService
{
    public Result<long> CalculateFactorial(int number)
    {
        if (number < 0)
        {
            return Result<long>.ErrorResult("Number must be non-negative.");
        }
      
        if (number > 20)
        {
            return Result<long>.ErrorResult("Number is too large to calculate factorial without overflow.");
        }

        long result = 1;
        for (int i = 1; i <= number; i++)
        {
            result *= i;
        }
        return Result<long>.SuccessResult(result);
    }

    public int[] Sort(params int[] numbers)
    {
        int n = numbers.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (numbers[j] > numbers[j + 1])
                {
                    int temp = numbers[j];
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = temp;
                }
            }
        }

        return numbers;
    }
}