
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
                shas.ForEach(sha =>
                { 
                    Console.WriteLine(Git.GetInfo(sha));
                });
      
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
