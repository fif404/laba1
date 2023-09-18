using System;
using System.Threading;

class Program
{
    static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static void SimplifyFraction(int a, int b, out int numerator, out int denominator)
    {
        int gcd = GCD(a, b);
        numerator = a / gcd;
        denominator = b / gcd;
    }

    static void SimplifyFractionParallel(int a, int b, out int numerator, out int denominator)
    {
        int gcd = 1;
        Thread thread1 = new Thread(() => gcd = GCD(a, b / 2));
        Thread thread2 = new Thread(() => gcd = GCD(a, b - b / 2));
        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();

        numerator = a / gcd;
        denominator = b / gcd;
    }

    static void Main(string[] args)
    {
        int a = 24;
        int b = 36;

        int numerator, denominator;

        DateTime startTime = DateTime.Now;
        SimplifyFraction(a, b, out numerator, out denominator);
        DateTime endTime = DateTime.Now;
        TimeSpan sequentialTime = endTime - startTime;

        Console.WriteLine("Последовательные вычисления:");
        Console.WriteLine($"Упрощенная дробь: {numerator}/{denominator}");
        Console.WriteLine($"Время выполнения: {sequentialTime.TotalMilliseconds} мс");

        startTime = DateTime.Now;
        SimplifyFractionParallel(a, b, out numerator, out denominator);
        endTime = DateTime.Now;
        TimeSpan parallelTime = endTime - startTime;

        Console.WriteLine("Параллельные вычисления:");
        Console.WriteLine($"Упрощенная дробь: {numerator}/{denominator}");
        Console.WriteLine($"Время выполнения: {parallelTime.TotalMilliseconds} мс");

        Console.ReadKey();
    }
}