using System;
using System.Text.RegularExpressions;
using CommitSize;
using NUnit.Framework;

namespace CommitSizeTests {
    public class ParseGitTests {
        Match stats = ParseGit.GetChangeMatch ("4 files changed, 161 insertions(+), 75 deletions(-)");
        Match noDeletions = ParseGit.GetChangeMatch ("4 files changed, 1 insertion(+)");
        Match noInsertions = ParseGit.GetChangeMatch ("4 files changed, 1 deletion(-)");
        Match badChangeMatch = ParseGit.GetChangeMatch ("This is a garbage string.");

        Match dateMatch1 = ParseGit.GetDateMatch ("Date:   2020-03-09 13:56:30 -0600");
        Match dateMatch2 = ParseGit.GetDateMatch ("Date:   2020-01-24 22:01:46 +0000");
        Match badDateMatch = ParseGit.GetDateMatch ("not a match string");

        [Test]
        public void GetChangeMatch_Correctly_Matches () {
            Match match1 = ParseGit.GetChangeMatch ("4 files changed, 161 insertions(+), 75 deletions(-)");
            Match match2 = ParseGit.GetChangeMatch ("not a matching string");
            Assert.IsTrue (match1.Success);
            Assert.IsFalse (match2.Success);
        }

        private void parseBadChangeMatch () {
            ParseGit.GetInfoFromMatch (badChangeMatch);
        }

        [Test]
        public void GetInfoFromMatch_Throws_FormatException_On_Bad_Match () {
            Assert.Throws (typeof (System.FormatException), new TestDelegate (parseBadChangeMatch));
        }

        [Test]
        public void GetInfoFromMatchs_Returns_Correct_File_Changes () {
            Assert.AreEqual (4, ParseGit.GetInfoFromMatch (stats).Files);
            Assert.AreEqual (4, ParseGit.GetInfoFromMatch (noDeletions).Files);
            Assert.AreEqual (4, ParseGit.GetInfoFromMatch (noInsertions).Files);
        }

        [Test]
        public void GetInfoFromMatch_Can_Parse_Insertions () {
            Assert.AreEqual (161, ParseGit.GetInfoFromMatch (stats).Insertions);
            Assert.AreEqual (1, ParseGit.GetInfoFromMatch (noDeletions).Insertions);
            Assert.AreEqual (0, ParseGit.GetInfoFromMatch (noInsertions).Insertions);
        }

        [Test]
        public void GetInfoFromMatch_Can_Parse_Deletions () {
            Assert.AreEqual (75, ParseGit.GetInfoFromMatch (stats).Deletions);
            Assert.AreEqual (1, ParseGit.GetInfoFromMatch (noInsertions).Deletions);
            Assert.AreEqual (0, ParseGit.GetInfoFromMatch (noDeletions).Deletions);
        }

        [Test]
        public void GetDateMatch_Correctly_Matches () {
            Assert.IsTrue (dateMatch1.Success);
            Assert.IsTrue (dateMatch2.Success);
            Assert.IsFalse (badDateMatch.Success);
        }

        private void parseBadDateMatch () {
            ParseGit.GetInfoFromMatch (badDateMatch);
        }

        [Test]
        public void GetDateFromMatch_Throws_FormatException_On_Bad_Match () {
            Assert.Throws (typeof (System.FormatException), new TestDelegate (parseBadDateMatch));
        }

        [Test]
        public void GetDateFromMatch_Gets_Correct_Date () {
            DateTime expectedDate = DateTime.Parse ("2020-03-09 13:56:30");
            Assert.AreEqual (expectedDate, ParseGit.GetDateFromMatch (dateMatch1));
        }

    }
}