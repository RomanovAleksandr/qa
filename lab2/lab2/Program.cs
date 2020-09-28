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

        static void ParseLinks(string content, List<string> links, List<string> newLinks)
        {
            string regex = "<a.*href=\"(.*?)\">?";
            foreach (Match match in Regex.Matches(content, regex))
            {
                string url = match.Groups[1].Value;
                url = root + url;
                if (!links.Contains(url) && !newLinks.Contains(url))
                {
                    links.Add(url);
                    newLinks.Add(url);
                }
            }
        }

        static void Request(string currentLink, List<string> links, List<string> newLinks, StreamWriter validLinksFile, StreamWriter invalidLinksFile)
        {
            try
            {

                WebRequest request = WebRequest.Create(currentLink);
                WebResponse response = request.GetResponse();
                if (request.RequestUri.Host != "91.210.252.240")
                {
                    return;
                }
                validLinksFile.WriteLine("{0} {1} {2}", currentLink, ((HttpWebResponse)response).StatusCode, DateTime.Now);
                string responseFromServer;
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }
                response.Close();
                ParseLinks(responseFromServer, links, newLinks);
            }
            catch (WebException e)
            {
                invalidLinksFile.WriteLine("{0} {1} {2}", currentLink, ((HttpWebResponse)e.Response).StatusCode, DateTime.Now);
            }             
        }

        static void Main(string[] args)
        {
            List<string> links = new List<string>();
            List<string> newLinks = new List<string>();
            links.Add(root);
            newLinks.Add(root);
            using (StreamWriter validLinksFile = new StreamWriter(@"..\..\..\..\valid.txt"))
            {
                using (StreamWriter invalidLinksFile = new StreamWriter(@"..\..\..\..\invalid.txt"))
                {
                    while (newLinks.Count != 0)
                    {
                        string currentLink = newLinks[0];
                        newLinks.RemoveAt(0);
                        Request(currentLink, links, newLinks, validLinksFile, invalidLinksFile);
                    }
                }
            }
            
        }
    }
}
