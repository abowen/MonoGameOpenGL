using System.Collections.Generic;
using System.Linq;

namespace MonoGame.Graphics.Common.Helpers
{
    public class CsvHelper
    {
        public static IEnumerable<IEnumerable<string>> ReadCsv(string csvFile)
        {
            var lines = System.IO.File.ReadAllLines(csvFile);
            var names = new List<List<string>>();
            foreach (var line in lines)
            {
                var lineNames = new List<string>();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    lineNames = line.Split(',').ToList();
                }
                names.Add(lineNames);
            }
            return names;
        }
    }
}
