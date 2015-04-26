using System;
using System.IO;
using System.Text;

namespace RosettaMoviesDesktop
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();
            var videoFilePath = args[0];
            var subtitleText = SubtitleDownloader.DownloadSubtitle(videoFilePath);
            if (string.IsNullOrWhiteSpace(subtitleText))
                return;
            var subtitleFileName = CreateSubtitleFileName(videoFilePath);
            File.CreateText(subtitleFileName).Write(subtitleText);

        }

        private static string CreateSubtitleFileName(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            var stringBuilder = new StringBuilder(fileInfo.FullName.Substring(0,fileInfo.FullName.LastIndexOf(fileInfo.Extension, StringComparison.OrdinalIgnoreCase)));
            stringBuilder.Append(".srt");
            return stringBuilder.ToString();
        }
    }
}
