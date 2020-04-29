using System;
using System.Collections.Generic;

namespace CommitSize {
    class Program {
        static void Main () {
            string[] args = Environment.GetCommandLineArgs ();
            //"/Users/Gina/Workspace/AuditTrails"
            try {
                Git repo = new Git (args[2]);
                List<string> shas = repo.GetCommitShas ();
                List<ChangeInfo> changes = new List<ChangeInfo> ();
                float insertions = 0;
                float deletions = 0;
                float files = 0;
                shas.ForEach (sha => {
                    ChangeInfo change = repo.GetInfo (sha);
                    insertions = insertions + change.Insertions;
                    deletions = deletions + change.Deletions;
                    files = files + change.Files;
                    changes.Add (change);
                });
                Console.WriteLine ($"Average Files Changed {files/shas.Count}");
                Console.WriteLine ($"Average Lines Added {insertions / shas.Count}");
                Console.WriteLine ($"Average Lines Removed {deletions / shas.Count}");

            } catch (Exception e) {
                Console.WriteLine (e);
            }

        }
    }
}