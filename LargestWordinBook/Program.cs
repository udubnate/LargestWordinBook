using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace LargestWordinBook
{
    class Program
    {

        public static Boolean isAlphaNumeric(char strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9]*$");
            Console.WriteLine(strToCheck.ToString());
            return rg.IsMatch(strToCheck.ToString());
        }

        static void Main(string[] args)
        {
            int wordLength = 0;
            string largestWord = "";

            string filePath = @"C:\Temp\books\aliceinwonderland.txt";
            string lastStringBuffer = "";

            using (FileStream fs = File.OpenRead(filePath))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b,0,b.Length) > 0)
                {
                    char[] delimiters = { ' ', ',', '-','\n','\r','"', '“', '”', '—', '/','.' };
                    string currentStringBuffer = temp.GetString(b);

                    //check if there is a split word
                    if (lastStringBuffer != "" && isAlphaNumeric(lastStringBuffer[lastStringBuffer.Length-1]) && isAlphaNumeric(currentStringBuffer[0]))
                    {
                        string[] lastwords = lastStringBuffer.Split(delimiters);
                        string lastword = lastwords[lastwords.Length - 1];
                        currentStringBuffer = currentStringBuffer.Insert(0, lastword);
                        Console.WriteLine("Found One");
                    }

                    lastStringBuffer = currentStringBuffer;

                    string[] words = currentStringBuffer.Split(delimiters);
                    foreach (string word in words)
                    {
                        if (word.Length > wordLength)
                        {
                            wordLength = word.Length;
                            largestWord = word;
                        }

                    }
                    
                    Console.WriteLine(temp.GetString(b));
                }
            }

            Console.WriteLine("Largest word is: " + largestWord + " , Length of " + wordLength);
        }
    }
}
