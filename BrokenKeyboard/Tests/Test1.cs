using System.Diagnostics;
using System.Text;

namespace Tests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Func<string, string> funcs;
            funcs = BrokenKeyboard.Printer.Print1;
            funcs += BrokenKeyboard.Printer.Print2;
            funcs += BrokenKeyboard.Printer.Print3;

            Tester(funcs);

            int n = 100000;
            string timeTest = StringGenerator(n);

            Console.WriteLine("Сравнение скорости на произвольной строке");
            Console.WriteLine("b и B встречаются так же часто, как и остальные символы");
            TimeTester(funcs, timeTest);

            StringBuilder sb = new();
            for (int i = 0; i < n / 2; i++)
                sb.Append('b');
            string b = sb.ToString();

            Console.WriteLine();
            Console.WriteLine("Сравнение скорости для плохого случая");
            Console.WriteLine("Треть от всех символов - b, почти все в конце строки");
            TimeTester(funcs, string.Concat(timeTest, b));
        }

       static string StringGenerator(int length)
        {
            StringBuilder sb = new(length);

            for(; length > 0; length--)
            {
                int n = Random.Shared.Next(1, 53) + 64;
                char c = n <= 26 + 64 ? (char)n : (char)(n + 6);
                sb.Append(c);
            }

            return sb.ToString();
        }

        static void Tester(Func<string, string> func)
        {
            string source = "bBqwertyQWERTYbBbB";
            string expected = "qwerQWER";
            string actual;

            foreach(Func<string, string> f in func.GetInvocationList())
            {
                actual = f(source);
                Assert.AreEqual(expected, actual);
            }

            source = "bBqwertybbBBQWERTYbBbB";
            expected = "qwQWER";
            foreach (Func<string, string> f in func.GetInvocationList())
            {
                actual = f(source);
                Assert.AreEqual(expected, actual);
            }
        }

        static void TimeTester(Func<string, string> func, string argument)
        {
            Stopwatch sw = new();

            foreach(Func<string,string> f in func.GetInvocationList())
            {
                sw.Restart();
                f(argument);
                sw.Stop();
                Console.WriteLine(sw.Elapsed + " - " + f.Method.Name);
            } 
        }
    }
}
