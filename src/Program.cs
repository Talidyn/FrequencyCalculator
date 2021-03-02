using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;

namespace FrequencyCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Entry-point
            Console.WriteLine("Enter the full path to your text (.txt) file, or enter '0' to quit:");
            string filePath = Console.ReadLine();

            // Loop continues to accept and process documents until exited.
            while (filePath != "0")
            {
                try
                {
                    List<string> wordList = new List<string>();

                    string file = Functions.IngestFile(filePath);

                    if (file != null)
                    {
                        file = Functions.RemoveNonAlphaCharacters(file);

                        wordList = Functions.CreateList(file);

                        wordList = Functions.RemoveStopWords(wordList);

                        wordList = Functions.RemoveNonAlphaCharacters(wordList);

                        wordList = Functions.StemWords(wordList);

                        // Remove stop words one more time to account for possible
                        // stemmed terms that may exist on the StopWords list.
                        wordList = Functions.RemoveStopWords(wordList);

                        Functions.ComputeFrequencyAndPrintSorted(wordList);
                    }
                    else
                    {
                        Console.WriteLine("There was a problem with the document. Please check your file and path, and try again.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("WARNING - There was an error during execution: \n" + ex);
                }
                finally
                {
                    Console.WriteLine("\nEnter the full path to your text file, or enter '0' to quit:");
                    filePath = Console.ReadLine();
                }
            }
        }
    }
}
