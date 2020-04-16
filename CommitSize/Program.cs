
using System;
using System.Collections.Generic;

namespace CommitSize
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //\d* files? changed,?( \d* insertions?\(\+\))?,?( \d* deletions?\(\-\))?
                List<string> shas = Git.GetCommitShas();
                List<ChangeInfo> changes = new List<ChangeInfo>();
                shas.ForEach(sha =>
                {
                    ChangeInfo change = Git.GetInfo(sha);
                    changes.Add(change);
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
