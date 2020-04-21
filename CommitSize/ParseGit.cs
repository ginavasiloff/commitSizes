using System;
using System.Text.RegularExpressions;

namespace CommitSize
{
    public class ParseGit
    {
        public static Match GetChangeMatch(string input)
        {
            Regex changelinePattern = new Regex(@"(?<filesChanged>\d*) files? changed,?( (?<insertions>\d*) insertions?\(\+\))?,?( (?<deletions>\d*) deletions?\(\-\))?");
            return changelinePattern.Match(input);
        }

        public static ChangeInfo GetInfoFromMatch(Match match)
        {
            try
            {
                ChangeInfo changes = new ChangeInfo();
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
                changes.Files = int.Parse(files);
                changes.Insertions = int.Parse(insertions);
                changes.Deletions = int.Parse(deletions);
                return changes;
            } catch(Exception e)
            {
                Console.WriteLine("Bad Match. Match is missing an expected value.");
                throw e;
            }
        }
    }
}
