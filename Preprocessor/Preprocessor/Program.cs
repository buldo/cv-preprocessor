using System;
using System.IO;
using System.Text;

namespace Preprocessor
{
    class Program
    {
        private static readonly byte[] FileEnd = Encoding.UTF8.GetBytes("\n");
        private static readonly byte[] Tab = Encoding.UTF8.GetBytes("\t");

        static void Main(string[] args)
        {
            var filesToProcess = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.tsv");
            foreach (var file in filesToProcess)
            {
                ProcessFile(file);
            }
        }

        private static void ProcessFile(string fileName)
        {
            var fileLines = File.ReadAllLines(fileName);
            using var file = File.OpenWrite(fileName);
            file.Write(Encoding.UTF8.GetBytes(fileLines[0]));
            file.Write(FileEnd);
            for (int i = 1; i < fileLines.Length; i++)
            {
                ProcessAndWriteLine(file, fileLines[i]);
            }
        }

        private static void ProcessAndWriteLine(FileStream file, string fileLine)
        {
            var lineParts = fileLine.Split('\t');
            file.Write(Encoding.UTF8.GetBytes(lineParts[0]));
            file.Write(Tab);

            file.Write(Encoding.UTF8.GetBytes(lineParts[1]));
            file.Write(Tab);

            ProcessAndWriteSentence(file, lineParts[2]);
            file.Write(Tab);

            file.Write(Encoding.UTF8.GetBytes(lineParts[3]));
            file.Write(Tab);

            file.Write(Encoding.UTF8.GetBytes(lineParts[4]));
            file.Write(Tab);

            file.Write(Encoding.UTF8.GetBytes(lineParts[5]));
            file.Write(Tab);

            file.Write(Encoding.UTF8.GetBytes(lineParts[6]));
            file.Write(Tab);

            file.Write(Encoding.UTF8.GetBytes(lineParts[7]));

            file.Write(FileEnd);
        }

        private static void ProcessAndWriteSentence(FileStream file, string sentence)
        {
            foreach (var c in sentence.ToLowerInvariant().ToCharArray())
            {
                if (IsAllowed(c))
                {
                    file.Write(Encoding.UTF8.GetBytes(new[] {c}));
                }
            }

            static bool IsAllowed(in char c)
            {
                return char.IsLetter(c) || c == '-' || c == ' ';
            }
        }
    }
}
