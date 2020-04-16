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

                if (String.IsNullOrWhiteSpace(insertions))
                {
                    insertions = "0";
                }

                if (String.IsNullOrWhiteSpace(deletions))
                {
                    deletions = "0";
                }

                try
                {
                    changes.Files = Int32.Parse(files);
                    changes.Insertions = Int32.Parse(insertions);
                    changes.Deletions = Int32.Parse(deletions);
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
