using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FrequencyCalculator.UnitTests
{
    /// <summary>
    /// While technically some of these tests are not unit tests,
    /// in the essence of time I decided these will do instead of leveraging mocks.
    /// </summary>
    public class Tests
    {
        #region Constants

        readonly string testFilePath = @".\test resources\testtext.txt";
        readonly string nonExistantFilePath = @".\none.txt";
        readonly string incorrectFileTypePath = @".\test resources\testrtf.rtf";
        #endregion

        [Test]
        public void IngestFile_TextFile_ReturnsExpectedList()
        {
            List<string> expectedList = new List<string>
            {
                "based", "on", "the", "logic,", "this", "will", "contain", "?", "15", "terms'", "in", "a", "*list1", "fourteen", "fifteen"
            };
            
            List<string> assertList = Functions.IngestFile(testFilePath);

            Assert.IsNotNull(assertList);
            Assert.AreEqual(15, assertList.Count());
            Assert.AreEqual(expectedList, assertList);
        }

        [Test]
        public void IngestFile_DoesNotExist_ReturnsNull()
        {
            List<string> assertList = Functions.IngestFile(nonExistantFilePath);

            Assert.IsFalse(File.Exists(nonExistantFilePath));
            Assert.IsNull(assertList);
        }

        [Test]
        public void RemoveStopWords_ListOfWords_ReturnsExpectedEditedList()
        {
            List<string> termList = new List<string>
            {
                "5", "this", "terms", "be", "this", "and", "left", "?", "down", "down'", "i'm", "myself"
            };
            List<string> expectedList = new List<string>
            {
                "5", "terms", "left", "?","down'",
            };

            List<string> assertList = Functions.RemoveStopWords(termList);

            Assert.IsNotNull(assertList);
            Assert.AreEqual(5, assertList.Count());
            Assert.AreEqual(expectedList, assertList);

        }

        [Test]
        public void RemoveNonAlphaCharacters_StringOfAlphanumerics_ReturnsOnlyAlphaChars()
        {
            List<string> anList = new List<string> { "Th2es4eL@et)ter+sSh} ould*&Be'All\\ThatI sLeftWithNoSpace  s" };
            List<string> expectedList = new List<string> { "TheseLettersShouldBeAllThatIsLeftWithNoSpaces" };

            List<string> assertList = Functions.RemoveNonAlphaCharacters(anList);

            Assert.NotNull(assertList);
            Assert.AreEqual(expectedList, assertList);
        }

        [Test]
        public void StemWords_NonStemmedWords_ReturnsStemmedTerms()
        {
            List<string> wordList = new List<string> { "jumped", "jumping", "jumps", "legislate", "alice" };
            List<string> stemmedWordList = new List<string> { "jump", "jump", "jump", "legisl", "alic" };

            List<string> assertList = Functions.StemWords(wordList);

            Assert.IsNotNull(assertList);
            Assert.AreEqual(stemmedWordList, assertList);
        }

        [Test]
        public void ComputeFrequencyAndSort_ListOfTerms_ReturnsSortedList()
        {
            List<string> wordList = new List<string> { "two", "two", "one", "three", "one", "one", };

            IOrderedEnumerable<IGrouping<string, string>> sortedList = Functions.ComputeFrequencyAndSort(wordList);

            Assert.IsNotNull(sortedList);
            Assert.AreEqual("one", sortedList.First().Key);
            Assert.AreEqual(3, sortedList.First().Count());
        }
    }
}