## Frequency Calculator
This project was created as a development project that reads in a text file, performs some editing and calculation, and then prints out the 20 most commonly occurring terms in descending order of frequency.

#### How It Works
This simple C# console application built on .NET Core is designed to run as a standalone executable. It takes in a path to a text (.txt) file, parses the data within the text file and outputs a string in lowercase. That string is then edited by removing any text character that is not either part of the 26 character modern English alphabet or a single-quote ( ' ). After this the string is converted into a list of remaining terms, and then evaluated against a pre-defined list of stop words--removing all matching occurances--before having all remaining single-quotes removed.

The list of remaining terms are then processed through a Porter Stemming (see: https://tartarus.org/martin/PorterStemmer/
) algorithm. Finally the list of terms is sorted based on the frequency of each term and the top 20 most common terms are printed out in descending order.

**Assumptions**

- The text file must be ingested in entirety, but transitioned to a normalized case to allow for best matching of stop words
- The stop words contain non-alphabetical characters, so removing all characters first would compromise correct removal of terms.
- The Stemming Algorithm may change terms to roots that align to the stop words, so remove stop words multiple times.
- Sorting by frequency should be verified to the user by also displaying the number of occurances.

---
### How to Run
#### Build
**Requirements**:
- .NET Core 3.1 or higher.

Option 1:
Download the latest code from the Master branch and build the solution `FrequencyCalculator.sln`
>Note: If you don't need to run Unit Tests you can just build the `FrequencyCalculator.csproj` project.

Option 2: Download the latest release [HERE](https://github.com/Talidyn/FrequencyCalculator/releases)

#### Runing the Program
1. Run the `FrequencyCalculator.exe` executable to start the program.
2. The program will ask the user for a file path. Two example files have been included in the repository to use, or enter the path to your own text file to read.
3. The program will print out the formatted list of terms in the console window.

[Example Files](https://github.com/Talidyn/FrequencyCalculator/tree/master/src/resources):
- text1.txt
- text2.txt

If an error is encountered, the program will return either the exception message or a curtousy message to the user and continue.

---
### Output
The printed list of terms will be displayed in descending order of frequency and display the total number of occurances in the file.

Table results of included example file `text1.txt`:

Word         | # of Occurances
-------------|---------------
us           | 11
peopl        | 10
right        | 10
govern       | 10
law          | 9
state        | 9
power        | 8
time         | 6
among        | 5
establish    | 5
refus        | 5
form         | 4
abolish      | 4
new          | 4
coloni       | 4
assent       | 4
larg         | 4
legislature  | 4
legisl       | 4
independ     | 4