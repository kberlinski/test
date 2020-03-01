using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace testcsharp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            foreach (var a  in args)
            {
                Console.WriteLine(a);
            }
            var emails = await GetEmails(args[0]);

            foreach(var email in emails)
            {
                Console.WriteLine(email);
            }
        }

        static async Task<IList<string>> GetEmails(string url)
        {
            var httpclient = new HttpClient();
            var response = await httpclient.GetAsync(url);

            var listOfEmails = new List<string>();
            //listOfEmails.Add(response.Content.ReadAsStringAsync().Result);
            // regex na stacku c# find email address in string
            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            RegexOptions.IgnoreCase);
            MatchCollection emailMatches = emailRegex.Matches(response.Content.ReadAsStringAsync().Result);


            foreach (var emailMatch in emailMatches)
            {
                listOfEmails.Add(emailMatch.ToString());
            }

            return listOfEmails;
        }
    }
}
