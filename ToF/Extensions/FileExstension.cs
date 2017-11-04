using System.IO;
using System.Linq;

namespace ToF
{
    public static class FileExstension
    {
        public static string[] ReadAllLinesExceptFirst(this string filename)
        {
            var lines = File.ReadAllLines(filename);
            if(lines.Count() > 0)
            {
                lines = lines.Skip(1).ToArray();
            }
            return lines;
        }
    }
}
