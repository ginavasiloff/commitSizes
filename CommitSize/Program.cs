
using System;
using System.Diagnostics;

namespace CommitSize
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = "/Users/Gina/Workspace/AuditTrails",
                    FileName = "git",
                    Arguments = "log",
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false
                };
                Process proc = Process.Start(startInfo);
                while(!proc.StandardOutput.EndOfStream)
                {
                    var line = proc.StandardOutput.ReadLine();
                    Console.WriteLine(line);
                }
                proc.Close();              
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
