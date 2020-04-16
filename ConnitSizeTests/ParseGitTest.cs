using NUnit.Framework;

namespace ConnitSizeTests
{
    public class ParseGitTests
    {
        private static string stats = "4 files changed, 161 insertions(+), 75 deletions(-)";
        private static string noDeletions = "4 files changed, 1 insertion(+)";
        private static string noInsertions = "4 files changed, 1 deletion(-)";


        [Test]
        public void ParseStats_Returns_Null_On_No_Stats()
        {
            string notStats = "This is a garbage string.";
            Assert.IsNull(CommitSize.ParseGit.ParseStat(notStats));
        }

        [Test]
        public void ParseStats_Returns_Correct_Commit_Changes()
        {
            Assert.AreEqual(4, CommitSize.ParseGit.ParseStat(stats).Files);
            Assert.AreEqual(4, CommitSize.ParseGit.ParseStat(stats).Files);
            Assert.AreEqual(4, CommitSize.ParseGit.ParseStat(stats).Files);
        }

        [Test]
        public void ParseStats_Can_Parse_Insertions()
        {
            Assert.AreEqual(161, CommitSize.ParseGit.ParseStat(stats).Insertions);
            Assert.AreEqual(1, CommitSize.ParseGit.ParseStat(noDeletions).Insertions);
            Assert.AreEqual(0, CommitSize.ParseGit.ParseStat(noInsertions).Insertions);
        }

        [Test]
        public void ParseStats_Can_Parse_Deletions()
        {
            Assert.AreEqual(75, CommitSize.ParseGit.ParseStat(stats).Deletions);
            Assert.AreEqual(1, CommitSize.ParseGit.ParseStat(noInsertions).Deletions);
            Assert.AreEqual(0, CommitSize.ParseGit.ParseStat(noDeletions).Deletions);
        }
    }
}