using System;
using System.IO;
using System.Diagnostics;

namespace TriangleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader input = new StreamReader(@"..\..\..\test.txt"))
            {
                using (StreamWriter output = new StreamWriter(@"..\..\..\output.txt"))
                {
                    int i = 1;
                    string arguments;
                    while ((arguments = input.ReadLine()) != null)
                    {
                        string expected = input.ReadLine();

                        var proc = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                //FileName = "C:\\Users\\Александр\\Documents\\2020\\qa\\lab1\\Triangle\\Triangle\\bin\\Debug\\netcoreapp3.1\\Triangle.exe",
                                FileName = @"..\..\..\..\Triangle\bin\Debug\netcoreapp3.1\Triangle.exe",
                                Arguments = arguments,
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                CreateNoWindow = true
                            }
                        };

                        proc.Start();
                        StreamReader reader = proc.StandardOutput;
                        string procOutput = reader.ReadToEnd();

                        if (procOutput.Trim() == expected)
                        {
                            output.WriteLine(i + " success");
                        }
                        else
                        {
                            output.WriteLine(i + " error");
                        }
                        i++;
                    }
                }
            }
        }
    }
}
