using CommitSize;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace CommitSizeTests
{
    public class ParseGitTests
    {
        private static Match stats = ParseGit.GetChangeMatch("4 files changed, 161 insertions(+), 75 deletions(-)");
        private static Match noDeletions = ParseGit.GetChangeMatch("4 files changed, 1 insertion(+)");
        private static Match noInsertions = ParseGit.GetChangeMatch("4 files changed, 1 deletion(-)");
        private static Match badMatch = ParseGit.GetChangeMatch("This is a garbage string.");

        private void triggerBadMatch()
        {
            ParseGit.GetInfoFromMatch(badMatch);
        }

        [Test]
        public void GetChangeMatch_Correctly_Matches()
        {
            Match match1 = ParseGit.GetChangeMatch("4 files changed, 161 insertions(+), 75 deletions(-)");
            Match match2 = ParseGit.GetChangeMatch("not a matching string");
            Assert.IsTrue(match1.Success);
            Assert.IsFalse(match2.Success);
        }

        [Test]
        public void GetInfoFromMatch_Throws_FormatException_On_Bad_Match()
        {
           Assert.Throws(typeof(System.FormatException), new TestDelegate(triggerBadMatch));
        }

        [Test]
        public void GetInfoFromMatchs_Returns_Correct_File_Changes()
        {
            Assert.AreEqual(4, ParseGit.GetInfoFromMatch(stats).Files);
            Assert.AreEqual(4, ParseGit.GetInfoFromMatch(noDeletions).Files);
            Assert.AreEqual(4, ParseGit.GetInfoFromMatch(noInsertions).Files);
 
        }

        [Test]
        public void GetInfoFromMatch_Can_Parse_Insertions()
        {
            Assert.AreEqual(161, ParseGit.GetInfoFromMatch(stats).Insertions);
            Assert.AreEqual(1, ParseGit.GetInfoFromMatch(noDeletions).Insertions);
            Assert.AreEqual(0, ParseGit.GetInfoFromMatch(noInsertions).Insertions);
        }

        [Test]
        public void GetInfoFromMatch_Can_Parse_Deletions()
        {
            Assert.AreEqual(75, ParseGit.GetInfoFromMatch(stats).Deletions);
            Assert.AreEqual(1, ParseGit.GetInfoFromMatch(noInsertions).Deletions);
            Assert.AreEqual(0, ParseGit.GetInfoFromMatch(noDeletions).Deletions);
        }
    }
}