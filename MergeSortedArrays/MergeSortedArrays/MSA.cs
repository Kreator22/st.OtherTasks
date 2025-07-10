namespace MergeSortedArrays
{
    public static class MSA
    {
        public static IEnumerable<int> Merge(int[] left, int[] right)
        {
            int ri = 0;

            if (right.Length < left.Length)
                (left, right) = (right, left);

            foreach (int num in left)
            {
                while (ri < right.Length && right[ri] <= num)
                {
                    if(right[ri] < num)
                        yield return right[ri];
                    ri++;
                }

                yield return num;
            }
        }
    }
}
