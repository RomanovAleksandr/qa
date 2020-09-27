using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace lab2
{
    class Program
    {
        static string root = "http://91.210.252.240/broken-links/";

        static void ParseLinks(string content, List<string> links)
        {
            string regex = "<a.*href=\"([^#][^@]*?)\".*>";
            foreach (var link in Regex.Matches(content, regex))
            {
                string url = Regex.Match(link.ToString(), regex).Groups[1].Value;
                Console.WriteLine(url);
            }
        }

        static List<string> Request(string link, List<string> links)
        {
            List<string> newLinks = new List<string>();
            WebRequest request = WebRequest.Create(link);
            WebResponse response = request.GetResponse();
            string responseFromServer;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
            }
            response.Close();
            ParseLinks(responseFromServer, links);

            return newLinks;
        }

        static void Main(string[] args)
        {
            List<string> links = new List<string>();
            links.Add(root);
            Request(root, links);

            Console.WriteLine("Hello World!");
        }
    }
}
