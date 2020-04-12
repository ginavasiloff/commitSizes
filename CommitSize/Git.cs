using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CommitSize
{
    public static class Git
    {
        public static List<string> GetCommitShas()
        {
            List<string> shas = new List<string>();
            ProcessStartInfo startInfo = GetStartInfo("log");
            Process log = Process.Start(startInfo);

            while (!log.StandardOutput.EndOfStream)
            {
                string line = log.StandardOutput.ReadLine();
                if (Regex.Match(line, "commit") != Match.Empty)
                {
                    string sha = line.Split("commit ")[1];
                    shas.Add(sha);
                }
            }
            log.Close();
            log.Dispose();
            return shas;
        }

        public static string GetInfo(string sha)
        {
            ProcessStartInfo startInfo = GetStartInfo($"show --stat {sha}");
            Process stats = Process.Start(startInfo);
            string line = "";
            while (!stats.StandardOutput.EndOfStream)
            {
                line = stats.StandardOutput.ReadLine();
                if (Regex.Match(line, @"\d* files? changed,?( \d* insertions?\(\+\))?,?( \d* deletions?\(\-\))?") != Match.Empty)
                {
                    break;
                }

            }
            stats.Close();
            stats.Dispose();
            return line;
        }

        private static ProcessStartInfo GetStartInfo(string args)
        {
            return new ProcessStartInfo()
            {
                WorkingDirectory = "/Users/Gina/Workspace/AuditTrails",
                FileName = "git",
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false
            };
        }
    }
}
