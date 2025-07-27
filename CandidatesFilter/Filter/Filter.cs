using System.Globalization;

namespace Filter
{
    public static class Filter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Хотя по заданию ожидаются строки, сигнатура метода взята напрямую
        /// из компилятора hh.ru в котором проверяется решение, т.е. на вход там 
        /// отдают Dictionary
        /// </remarks>
        public static List<string> SelectCandidates(Dictionary<string, int[]> candidates)
        {
            var result = candidates
                .Select(c => (
                    name : c.Key, 
                    average : Double.Round( (double)c.Value.Sum() / (double)c.Value.Length, 1)))
                .Where(c => c.average >= 5.0)
                .OrderBy(c => -c.average)
                .ThenBy(c => c.name)
                .Select(c => c.name + "," + c.average.ToString("F1", CultureInfo.InvariantCulture))
                .ToList();

            if (!result.Any())
                result.Add("Никто");

            return result;
        }
    }
}
