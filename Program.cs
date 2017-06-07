using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample03
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }
        public void Run()
        {
            Console.WriteLine("Start Run");
            const int arrSize = 500000;
            const int breakIndex = 300000 - 1;
            int[] arr = new int[arrSize];

            ParallelLoopResult loopResult = Parallel.For(0, arrSize, (index, loopState) =>
                    {
                        DoSomeWork(index);
                        arr[index] = 1;
                        if (index == breakIndex)
                        {
                            Console.WriteLine("Stop/Break");
                            loopState.Break();
                            // loopState.Stop();
                        }
                    });

            Console.WriteLine("IsCompleted: {0}", loopResult.IsCompleted);
            Console.WriteLine("LowestBreakIteration: {0}", loopResult.LowestBreakIteration);
            int elements = 0;
            for (int i = 0; i <= breakIndex; i++)
                elements += arr[i];
            Console.WriteLine("Elements: {0}", elements);

            elements = 0;
            for (int i = breakIndex + 1; i < arrSize; i++)
                elements += arr[i];
            Console.WriteLine("Elements: {0}", elements);

            Console.WriteLine("End Run");
            Console.ReadLine();
        }
        private void DoSomeWork(int index)
        {
            double temp = 1.1;
            for (int i = 0; i < 100; i++)
                temp = Math.Sin(index) + Math.Sqrt(index) * Math.Pow(index, 3.1415) + temp;
        }
    }
}
