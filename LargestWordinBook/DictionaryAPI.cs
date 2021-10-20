using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace LargestWordinBook
{
    class DictionaryAPI
    {
        private string baseurl = "https://api.dictionaryapi.dev/api/v2/entries/en/";
        private string word = "any";

        public DictionaryAPI(string word)
        {
            this.word = word;
        }

        public DictionaryObject[] GetDictionary()
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    var json = wc.DownloadString(baseurl + this.word);
                    var dictobj = DictionaryObject.FromJson(json);
                    return dictobj;
                }
                catch
                {
                    Console.WriteLine("No results were found for the definition.");
                    return null;
                }
                
            }
        }

        public void OutputDefinition(DictionaryObject[] dictObj)
        {
            int count = 0;

            foreach (DictionaryObject dict in dictObj)
            {
                foreach (var meaning in dict.Meanings)
                {
                    foreach (var definition in meaning.Definitions)
                    {
                        Console.Write(++count + ": ");
                        Console.WriteLine(definition.DefinitionDefinition);

                    }
                }
            }
        }
    }

}
