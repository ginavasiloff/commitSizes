using System;
using System.Text.RegularExpressions;

namespace CommitSize
{
    public class ParseGit
    {
        public static ChangeInfo ParseStat(string input)
        {
            ChangeInfo changes = new ChangeInfo();
            Regex changelinePattern = new Regex(@"(?<filesChanged>\d*) files? changed,?( (?<insertions>\d*) insertions?\(\+\))?,?( (?<deletions>\d*) deletions?\(\-\))?");
            Match match = changelinePattern.Match(input);
            if (match.Success)
            {
                string files = match.Groups["filesChanged"].Value;
                string insertions = match.Groups["insertions"].Value;
                string deletions = match.Groups["deletions"].Value;

                if (string.IsNullOrWhiteSpace(insertions))
                {
                    insertions = "0";
                }

                if (string.IsNullOrWhiteSpace(deletions))
                {
                    deletions = "0";
                }

                try
                {
                    changes.Files = int.Parse(files);
                    changes.Insertions = int.Parse(insertions);
                    changes.Deletions = int.Parse(deletions);
                } catch(System.FormatException e)
                {
                    Console.WriteLine("Bad Format: ");
                    Console.WriteLine(input);
                }
                    return changes;
                
            }
            return null;
        }
    }
}
