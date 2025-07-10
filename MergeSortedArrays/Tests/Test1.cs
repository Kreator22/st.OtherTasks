using MergeSortedArrays;
using System.Text;

namespace Tests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void MergeSortedArrays()
        {
            int[] left = [0, 1, 2, 3, 4, 8, 9];
            int[] right = [3, 4, 5, 6, 7, 10];

            int[] actual = MSA.Merge(left, right).ToArray();
            int[] expected = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

            StringBuilder sb = new();
            foreach (int a in actual)
                sb.Append(a + ", ");
            Console.WriteLine(sb.ToString().Trim([' ', ',']));

            CollectionAssert.AreEqual(expected, actual);

            actual = MSA.Merge(right, left).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
