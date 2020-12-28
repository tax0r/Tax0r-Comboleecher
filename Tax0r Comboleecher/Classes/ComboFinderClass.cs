using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tax0r_Comboleecher.Classes
{
    class ComboFinderClass
    {
        public async Task<string> GetUrlContent(string url)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(5);
            string content = await httpClient.GetStringAsync(url);
            return content;
        }

        public string[] GetCombos(string content)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)+:[a-zA-Z0-9]*", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(content);
            List<string> resultsList = new List<string>();

            foreach (Match match in matches)
            {
                resultsList.Add(match.ToString());
            }

            string[] results = resultsList.ToArray();
            return results;
        }
    }
}
