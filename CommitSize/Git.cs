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

        public static ChangeInfo GetInfo(string sha)
        {
            ProcessStartInfo startInfo = GetStartInfo($"show --stat {sha}");
            Process stats = Process.Start(startInfo);
            ChangeInfo changes = new ChangeInfo();
            while (!stats.StandardOutput.EndOfStream)
            {
                changes = ParseGit.ParseStat(stats.StandardOutput.ReadLine());
                changes.Commit = sha;
            }
            stats.Close();
            stats.Dispose();
            return changes;
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

public class ChangeInfo
{
    public string Commit;
    public int Files = 0;
    public int Deletions = 0;
    public int Insertions = 0;
}