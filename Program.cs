using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsFileConcatenator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any() &&
                args[0].Contains("?")) 
            { 
                PrintHelp();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return;
            }

            var currentDirectory = Environment.CurrentDirectory;
            Console.WriteLine("Current Directory: {0}", currentDirectory);
            var files = Directory.GetFiles(currentDirectory, "*.ts");
            files.ToList().ForEach(f=> Console.WriteLine("ts file: {0}", f));

            if (files.Any())
            {
                var outputFile = Path.Combine(currentDirectory, "combined.ts");
                Console.WriteLine("Resultant file: {0}", outputFile);

                ConcatenateFiles(outputFile, files);
            }
            else
            {
                Console.WriteLine("No ts files to concatenate in: {0}", currentDirectory);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void ConcatenateFiles(string outputFile, IEnumerable<string> inputFiles)
        {
            using (Stream output = File.OpenWrite(outputFile))
            {
                foreach (var inputFile in inputFiles)
                {
                    if (!File.Exists(inputFile))
                    {
                        Console.WriteLine(string.Format("File Not Found: {0}", inputFile));
                        continue;
                    }
                    using (var input = File.OpenRead(inputFile))
                        input.CopyTo(output);
                }
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine("Concatenates all ts in the Current Directory.");
            Console.WriteLine("Creates a new file called combined.ts");

        }
    }
}
