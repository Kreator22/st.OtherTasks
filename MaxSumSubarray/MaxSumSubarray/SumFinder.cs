using System.Formats.Asn1;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace MaxSumSubarray
{
    public class SumFinder
    {
        /// <summary>
        /// Наибольшая сумма чисел подмассива из двух типов чисел
        /// </summary>
        /// <remarks>
        /// Сложность по времени - O(n)
        /// Сложность по памяти в худшем случае O(n)
        /// (весь входной массив состоит из чисел двух типов)
        /// </remarks>
        public static ulong Finder1(uint[] arr)
        {
            //Два известных типов чисел входящие в текущую скользящую сумму
            uint firstType = 0, secondType = 0;
            //Числа входящие в текущую скользящую сумму
            Queue<uint> nums = new();
            ulong sum = 0, maxSum = 0;

            foreach(uint num in arr)
            {
                if (num == firstType || num == secondType)
                    nums.Enqueue(num);
                else
                {
                    sum = 0;
                    foreach (var n in nums)
                        sum += n;
                    maxSum = sum > maxSum ? sum : maxSum;

                    while (nums.Contains(firstType))
                        nums.Dequeue();
                    (firstType, secondType) = (secondType, num);
                    nums.Enqueue(secondType);
                }
            }

            //Последние два типа чисел оставшиеся в очереди
            sum = 0;
            while (nums.Any())
                sum += nums.Dequeue();
            maxSum = sum > maxSum ? sum : maxSum;

            return maxSum;
        }

        /// <summary>
        /// Наибольшая сумма чисел подмассива из двух типов чисел
        /// </summary>
        /// <remarks>
        /// Сложность по времени - O(n)
        /// Сложность по памяти O(1)
        /// Решение написано после просмотра решений других людей
        /// </remarks>
        public static ulong Finder2(uint[] arr)
        {
            //left и right - тип числа и количество его повторений в последней паре
            //типов чисел для текущей скользящей суммы
            //last - тип и количество повторений right подряд без прерываний
            (uint type, int repeats) left, right, last;
            (left, right, last) = ((0, 0), (0, 0), (0, 0));

            ulong maxSum = 0;

            foreach (uint num in arr)
            {
                if(num == right.type)
                {
                    right.repeats++;
                    last.repeats++;
                    continue;
                }

                if(num == left.type)
                {
                    left.repeats++;
                    (left, right) = (right, left);
                    (last.type, last.repeats) = (right.type, 1);
                    continue;
                }

                maxSum = MaxSum(maxSum, left, right);

                (left, right, last) = (last, (num, 1), (num, 1));
            }

            //Последние два типа чисел оставшиеся в очереди
            maxSum = MaxSum(maxSum, left, right);

            static ulong MaxSum( 
                ulong currentMaxSum,
                (uint type, int repeats) left, 
                (uint type, int repeats) right)
            {
                ulong sum = (ulong)(left.type * left.repeats + right.type * right.repeats);
                return sum > currentMaxSum ? sum : currentMaxSum;
            }

            return maxSum;
        }
    }
}
