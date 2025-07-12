using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public sealed class MaxSumSubarrayTests
    {
        public void TestMethods(Func<uint[], ulong> func)
        {
            uint[][] sources = [
                [1, 2, 3],
                [1, 1, 1, 1, 2, 3],
                [1, 1, 2, 2, 3, 3],
                [1, 1, 1, 1, 2, 3, 3],
                [1, 2, 1, 2, 1, 2, 3],
                [3, 1, 2, 1, 2, 1, 2]
                ];
            ulong[] expected = [5, 6, 10, 8, 9, 9];

            uint[] source = new uint[100000000];
            for (int i = 0; i < source.Length; i++)
                source[i] = (uint)Random.Shared.Next(10);

            foreach (Func<uint[], ulong> f in func.GetInvocationList())
            {
                List<ulong> actuals = new();

                for (int i = 0; i < sources.Length; i++)
                    actuals.Add( f(sources[i]) );

                CollectionAssert.AreEquivalent(expected, actuals);

                Stopwatch sw = new();
                sw.Restart();
                var sum = f(source);
                sw.Stop();

                Console.WriteLine(f.Method.Name + " - " + sw.ElapsedMilliseconds);
            }            
        }

        [TestMethod]
        public void MaxSumSubarrayTest()
        {
            Func<uint[], ulong> func = MaxSumSubarray.SumFinder.Finder1;
            func += MaxSumSubarray.SumFinder.Finder2;

            TestMethods(func);
        }
    }
}
