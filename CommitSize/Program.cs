
using System;
using System.Collections.Generic;

namespace CommitSize
{
    class Program
    {
        static void Main()
        {
            //string[] args = Environment.GetCommandLineArgs();
            string[] args = { "/Users/Gina/Workspace/AuditTrails" };
            try
            {
                Git repo = new Git(args[0]);
                List<string> shas = repo.GetCommitShas();
                List<ChangeInfo> changes = new List<ChangeInfo>();
                shas.ForEach(sha =>
                {
                    Console.WriteLine(sha);
                    ChangeInfo change = repo.GetInfo(sha);
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
