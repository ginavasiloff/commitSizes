using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CommitSize {
    public class Git {
        public string workingDirectory { get; set; }

        public Git (string workingDirectory) {
            this.workingDirectory = workingDirectory;
        }

        public List<string> GetCommitShas () {
            List<string> shas = new List<string> ();
            ProcessStartInfo startInfo = GetStartInfo ("log --date=iso");
            Process log = Process.Start (startInfo);

            while (!log.StandardOutput.EndOfStream) {
                string line = log.StandardOutput.ReadLine ();
                if (Regex.Match (line, "commit") != Match.Empty) {
                    string sha = line.Split ("commit ") [1];
                    shas.Add (sha);
                }
            }
            log.Close ();
            log.Dispose ();
            return shas;
        }

        public ChangeInfo GetInfo (string sha) {
            ProcessStartInfo startInfo = GetStartInfo ($"show --stat {sha}");
            Process stats = Process.Start (startInfo);
            while (!stats.StandardOutput.EndOfStream) {
                string line = stats.StandardOutput.ReadLine ();
                Match changeMatch = ParseGit.GetChangeMatch (line);
                Match dateMatch = ParseGit.GetDateMatch (line);
                if (changeMatch.Success) {
                    return ParseGit.GetInfoFromMatch (changeMatch);
                }
            }
            stats.Close ();
            stats.Dispose ();
            return null;
        }

        private ProcessStartInfo GetStartInfo (string gitArgs) {
            return new ProcessStartInfo () {
                WorkingDirectory = workingDirectory,
                    FileName = "git",
                    Arguments = gitArgs,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false
            };
        }
    }
}

public class ChangeInfo {
    public int Files = 0;
    public int Deletions = 0;
    public int Insertions = 0;
}