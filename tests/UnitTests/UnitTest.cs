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
        #endregion
        
        [Test]
        public void IngestFile_TextFile_ReturnsExpectedString()
        {
            string ingestedFile = Functions.IngestFile(testFilePath);
            string expectedString = "alice's adventures in wonderland\n" +
                "lewis carroll\n" +
                "\n" +
                "chapter 1 - down the rabbit-hole";

            Assert.IsNotNull(ingestedFile);
            Assert.AreEqual(expectedString, ingestedFile);
        }

        [Test]
        public void IngestFile_DoesNotExist_ReturnsNull()
        {
            string file = Functions.IngestFile(nonExistantFilePath);

            Assert.IsNull(file);
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
        public void RemoveNonAlphaCharacters_StringOfAlphanumerics_ReturnsOnlyAlphaCharsAndSingleQuotes()
        {
            string anString = "'These%letters--should$be`|left'with 'single-quotes'";
            string expectedString = "'These letters  should be  left'with 'single quotes'";

            string assertString = Functions.RemoveNonAlphaCharacters(anString);

            Assert.NotNull(assertString);
            Assert.AreEqual(expectedString, assertString);
        }

        [Test]
        public void RemoveNonAlphaCharacters_ListOfAlphanumericWords_ReturnsOnlyAlphaCharacters()
        {
            List<string> wordList = new List<string>
            {
                "the se", "this's", " ", "be", "th&is", "and", "l7eft ", "?", "down", "down'", "i'm", "myself"
            };
            List<string> expectedList = new List<string>
            {
                "these", "thiss", "be", "this", "and", "left", "down", "down", "im", "myself"
            };

            List<string> assertList = Functions.RemoveNonAlphaCharacters(wordList);

            Assert.IsNotNull(assertList);
            Assert.AreEqual(expectedList, assertList);
            Assert.That(assertList.Count() == 10);
        }

        [Test]
        public void StemWords_NonStemmedWords_ReturnsStemmedTerms()
        {
            List<string> wordList = new List<string> { "jumped", "jumping", "jumps", "legislate", "alice", "once", "hope" };
            List<string> stemmedWordList = new List<string> { "jump", "jump", "jump", "legisl", "alice", "onc", "hope" };

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
            Assert.AreEqual("two", sortedList.ElementAt(1).Key);
            Assert.AreEqual(2, sortedList.ElementAt(1).Count());
        }
    }
}