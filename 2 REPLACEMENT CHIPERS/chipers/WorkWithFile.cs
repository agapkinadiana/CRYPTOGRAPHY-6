using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace chipers
{
    public class WorkWithFile
    {
        public static string GetTextFromFile(string path, bool includeNumber = false)
        {
            string file = "";
            using (var streamReader = new StreamReader(path, Encoding.UTF32))
            {
                file = streamReader.ReadToEnd().ToLower();
                file = Regex.Replace(file, @"\W+", "");
                file = Regex.Replace(file, "v", "");
                if (!includeNumber)
                    file = Regex.Replace(file, @"\d+", "");
                var countLettersInFile = file.Count();
            }

            return file;
        }

        public static void SetTextToFile(string str, string path)
        {
            using (var streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(str.ToCharArray());
            }
        }
    }
}
