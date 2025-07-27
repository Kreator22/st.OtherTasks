using static Filter.Filter;

namespace FilterTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var candidates = new Dictionary<string, int[]>();
            candidates.Add("Ivanov", [5, 6, 7, 8]);
            candidates.Add("Lisii", [9, 8, 10, 9]);
            candidates.Add("Sokolova", [5, 6, 8, 5]);
            candidates.Add("Tritonov", [7, 2, 3, 4]);
            candidates.Add("Chernov", [8, 8, 8, 8]);
            candidates.Add("Svetova", [4, 5, 3, 6]);
            candidates.Add("Zayats", [5, 5, 5, 5]);
            candidates.Add("Rezhik", [6, 6, 6, 6]);
            candidates.Add("Test", [10, 10, 9]);

            List<string> actual = new();
            actual = SelectCandidates(candidates);

            foreach (var can in actual)
                Console.WriteLine(can);
        }
    }
}
