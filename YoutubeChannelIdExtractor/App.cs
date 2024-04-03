using System.IO;

namespace YoutubeChannelIdExtractor;
class App
{
    private readonly string[] _args;

    public App(in string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Error: No input given");
            Console.Write("Read Documentation for usage Explanation");
            Environment.Exit(1);
            throw new ArgumentException(nameof(args));
        }

        if (args.Length > 2)
        {
            Console.WriteLine("Error: To many inputs given");
            Console.WriteLine("Read Documentation for usage Explanation");
            Environment.Exit(2);
            throw new ArgumentException(nameof(args));
        }
        _args = args;
    }
    public void Run()
    {
        // 2 modes
        //mode 1 only url or file is given outputs only to std:out
        //mode 2 url or file and output file is given output to std:out and the output file

        //Run single mode
        if (_args.Length == 1)
        {
            InputProcessor proc = new InputProcessor();
            if (!IsFilePath(_args[0]))
            {
                string id = proc.GetId(_args[0]);
                Console.WriteLine(id);
            }
            else
            {
                IEnumerable<string> outputs = proc.ProcessFile(_args[0]);
                foreach (string line in outputs)
                {
                    Console.WriteLine(line);
                }
            }
        }

        // Run dual mode
        if (_args.Length == 2)
        {
            if (!IsFilePath(_args[1]))
            {
                throw new ArgumentException("The second Parameter is not a filepath");
            }
            FileInfo info = new FileInfo(_args[1]);
            InputProcessor proc = new InputProcessor();
            if (!IsFilePath(_args[0]))
            {
                string id = proc.GetId(_args[0]);
                File.AppendAllText(info.FullName, id);
                Console.WriteLine(id);
            }
            else
            {
                IEnumerable<string> outputs = proc.ProcessFile(_args[0]);
                File.AppendAllLines(info.FullName, outputs);
                foreach (string line in outputs)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }

    private bool IsFilePath(string input)
    {
        // Check if the path is a valid file path
        try
        {
            // Create a new FileInfo object with the provided path
            FileInfo fileInfo = new FileInfo(input);

            // Check if the file exists (this also implicitly checks if the path is valid)
            return fileInfo.Exists;
        }
        catch (Exception)
        {
            // If any exception occurs, return false
            return false;
        }
    }
}
