using System.Text;

namespace BrokenKeyboard
{
    public static class Printer
    {
        /// <summary>
        /// Сложность по времени - в худшем случае О(n^2) 
        /// (для каждого символа придётся проверить все остальные до начала строки),
        /// в лучшем случае O(n)
        /// (для каждой b или B символ для удаления - предыдущий)
        /// Сложность по памяти - O(n)
        /// </summary>
        public static string Print1(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            char[] chars = input.ToArray();

            for ((int i, int j) = (0, 0); i < chars.Length; i++)
            {
                if (chars[i] == 'b')
                {
                    j = i;
                    while (j > 0 && (char.IsUpper(chars[--j]) || chars[j] == ' ')) ;      
                    chars[j] = ' ';
                    chars[i] = ' ';
                    continue;
                }

                if (chars[i] == 'B')
                {
                    j = i;
                    while (j > 0 && (char.IsLower(chars[--j]) || chars[j] == ' ')) ;
                    chars[j] = ' ';
                    chars[i] = ' ';
                    continue;
                }
            }

            return new string(chars.Where(c => c != ' ').ToArray());
        }

        /// <summary>
        /// Сложность по времени - O(n)
        /// Сложность по памяти - O(n)
        /// </summary>
        public static string Print2(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            Stack<int> lowPos = new();
            Stack<int> upPos = new();

            char[] chars = input.ToArray();

            for(int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == 'b')
                {
                    if (lowPos.Count > 0)
                        chars[lowPos.Pop()] = ' ';
                    chars[i] = ' ';
                    continue;
                }
                
                if (chars[i] == 'B')
                {
                    if (upPos.Count > 0)
                        chars[upPos.Pop()] = ' ';
                    chars[i] = ' ';
                    continue;
                }

                else if (char.IsLower(chars[i]))
                    lowPos.Push(i);
                else
                    upPos.Push(i);
            }

            return new string(chars.Where(c => c != ' ').ToArray());
        }

        /// <summary>
        /// Решение отсюда:
        /// https://gist.github.com/DimsFromDergachy/28e501cb57557eaf070a67eac22e5e8f
        /// </summary>
        public static string Print3(string a)
        {
            // Stacks keep good indexes
            var lowers = new Stack<(int, char chr)>(a.Count());
            var uppers = new Stack<(int, char chr)>(a.Count());

            for (int i = 0; i < a.Count(); i++)
            {
                var ch = a[i];
                switch (ch)
                {
                    case 'b':
                        lowers.TryPop(out _); break;
                    case 'B':
                        uppers.TryPop(out _); break;
                    case var o when char.IsLower(o):
                        lowers.Push((i, ch)); break;
                    case var o when char.IsUpper(o):
                        uppers.Push((i, ch)); break;
                    default:
                        throw new NotSupportedException($"Unsupported character: '{ch}'");
                }
            }

            var result = new Stack<char>(a.Length);

            while (lowers.Any() || uppers.Any())
            {
                if (!lowers.Any())
                {
                    result.Push(uppers.Pop().chr); continue;
                }
                if (!uppers.Any())
                {
                    result.Push(lowers.Pop().chr); continue;
                }

                (int i1, _) = lowers.Peek();
                (int i2, _) = uppers.Peek();

                if (i1 > i2)
                {
                    result.Push(lowers.Pop().chr); continue;
                }
                if (i1 < i2)
                {
                    result.Push(uppers.Pop().chr); continue;
                }

                throw new NotSupportedException("Broken invariant");
            }

            return new string(result.ToArray());
        }

    }
}
