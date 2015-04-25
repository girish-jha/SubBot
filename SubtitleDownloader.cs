using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using RosettaMovies.Extentions;
using RosettaMoviesDesktop.Extentions;

namespace RosettaMoviesDesktop
{
    public class SubtitleDownloader
    {
        const string UriString = "http://api.thesubdb.com/";
        private const string UserAgent = "SubDB/1.0 (SubBot/0.1; http://github.com/girish-jha/SubBot)";

        public static string DownloadSubtitle(string filePath)
        {
            var hashKey = GenerateHashKey(filePath);
            if (hashKey == null) return null;

            var uri = new Uri(UriString).ApplyQueryStringForDownload(hashKey);
            var request = CreateWebRequest(uri);
            var str = "";
            try
            {
                var webResponse = request.GetResponse();
                str = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
            }
            catch (WebException e)
            {
                MessageBox.Show("No subtitle found!", "Error", MessageBoxButtons.OK);
            }
            
            return str;

        }

        private static string GenerateHashKey(string filePath)
        {
            string hashKey;
            try
            {
                hashKey = File.OpenRead(filePath).GenerateHashkey();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("you do not have sufficient permission to read the file", "Error", MessageBoxButtons.OK);
                return null;
            }
            catch (Exception)
            {
                MessageBox.Show("Not a valid file for subtitles", "Error", MessageBoxButtons.OK);
                return null;
            }
            return hashKey;
        }

        private static HttpWebRequest CreateWebRequest(Uri uri)
        {
            var request = WebRequest.CreateHttp(uri);
            request.Method = WebRequestMethods.Http.Get;
            request.UserAgent = UserAgent;
            return request;
        }
    }
}