using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace FrequencyCalculator
{
    public static class Functions
    {
        /// <summary>
        /// Reads in a text file and returns a string in lowercase.
        /// </summary>
        /// <param name="filePath">Path to the text file</param>
        /// <returns></returns>
        public static string IngestFile(string filePath)
        {
            filePath = filePath.Trim('\"');
            if (File.Exists(filePath) && Path.GetExtension(filePath).Equals(".txt"))
            {
                // Read as lowercase to standardize list of terms and increase stop word compatibility.
                string file = File.ReadAllText(filePath).ToLower();

                return file;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a list of words from the supplied string.
        /// The words are split on spaces, newlines, and returns.
        /// Null and Whitespace elements are removed.
        /// </summary>
        /// <param name="text">String to create a list from</param>
        /// <returns></returns>
        public static List<string> CreateList(string text)
        {
            List<string> wordList = new List<string>();

            string[] splitStrings = new string[] {" ", "\n", "\r"};
            foreach (var item in text.Split(splitStrings, StringSplitOptions.RemoveEmptyEntries))
            {
                wordList.Add(item.Trim('\''));
            }
            wordList.RemoveAll(string.IsNullOrEmpty);

            return wordList;
        }

        /// <summary>
        /// Removes all terms that equate to any words in the list of Stop Words. 
        /// </summary>
        /// <param name="wordList">List of terms to edit</param>
        /// <returns></returns>
        public static List<string> RemoveStopWords(List<string> wordList)
        {
            // Retrieve Stop Words from local resource to allow convenient editing if needed.
            string[] splitStrings = new string[] {" ", "\r", "\n"};
            foreach (var line in Properties.Resources.stopwords.Split(splitStrings, StringSplitOptions.RemoveEmptyEntries))
            {
                wordList.RemoveAll(x => x.Equals(line.Trim()));
            }
            
            return wordList;
        }

        /// <summary>
        /// Removes all non-alphabetical characters from the string except for single-quotes.
        /// </summary>
        /// <param name="text">String of text to edit</param>
        /// <returns></returns>
        public static string RemoveNonAlphaCharacters(string text)
        {
            string regex = @"[^a-zA-Z']";

            string alphaText = Regex.Replace(text, regex, " ");
            
            return alphaText;
        }

        /// <summary>
        /// Removes all non-alphabetical characters from the list of terms.
        /// Null and Whitespace elements are removed.
        /// </summary>
        /// <param name="wordList">List of terms to edit</param>
        /// <returns></returns>
        public static List<string> RemoveNonAlphaCharacters(List<string> wordList)
        {
            string regex = "[^a-zA-Z]";
            List<string> newList = new List<string>();

            foreach (var item in wordList)
            {
                newList.Add(Regex.Replace(item, regex, "").Trim());
            }
            newList.RemoveAll(string.IsNullOrEmpty);

            return newList;
        }

        /// <summary>
        /// Transforms the list of terms into their root form.
        /// Uses the Porter Stemming alogorithm.
        /// </summary>
        /// <param name="wordList">List of terms to edit</param>
        /// <returns></returns>
        public static List<string> StemWords(List<string> wordList)
        {
            var stemmer = new PorterStemmer();
            
            List<string> stemmedList = new List<string>();

            foreach (var item in wordList)
            {
                stemmedList.Add(stemmer.StemWord(item));
            }

            return stemmedList;
        }

        /// <summary>
        /// Computes the frequency of all terms in the list and sorts them in decending order of frequency.
        /// </summary>
        /// <param name="wordList">List of terms to compute</param>
        /// <returns></returns>
        public static IOrderedEnumerable<IGrouping<string, string>> ComputeFrequencyAndSort(List<string> wordList)
        {
            var sortedList = wordList
               .GroupBy(word => word)
               .OrderByDescending(group => group.Count());

            return sortedList;
        }

        /// <summary>
        /// Computes the frequency of all terms in the list.
        /// Prints the top 20 most commonly occuring terms in descending order.
        /// </summary>
        /// <param name="wordList">List of terms to compute</param>
        public static void ComputeFrequencyAndPrintSorted(List<string> wordList)
        {
            var sortedList = ComputeFrequencyAndSort(wordList);

            Console.WriteLine("\n{0, -12} | {1, -4}", "Word", "# of Occurances");
            Console.WriteLine("-------------|---------------");
            foreach (var item in sortedList.Take(20))
            {
                Console.WriteLine("{0, -12} | {1, -4}", item.Key, item.Count());
            }
        }
    }
}
