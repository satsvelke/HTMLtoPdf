using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Sats.HTMLtoPdf
{
    public static class Chrome
    {
        public static byte[] Pdf(this List<Options> options, ChromeDetails chromeDetails)
        {
            if (options.Count == 0)
                return null;


            var chromeOptions = options.Select(c => c.ChromeOptions).FirstOrDefault();


            if (chromeOptions.Count() == 0)
                return null;

            var arguments = string.Join(" ", chromeOptions.Select(c => c.ToString()));


            var output = !string.IsNullOrWhiteSpace(chromeDetails.OutputPath) ? chromeDetails.OutputPath :
                Path.Combine(Environment.CurrentDirectory, $"printout_{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()}.pdf");

            using (var p = new Process())
            {
                p.StartInfo.FileName = chromeDetails.ChromePath;
                p.StartInfo.Arguments = $"{arguments} --print-to-pdf={output} {chromeDetails.HtmlPath}";
                p.Start();
                p.WaitForExit();
            }

            if (File.Exists(output))
            {
                var file = File.ReadAllBytes(output);
                File.Delete(output);

                return file;
            }
            else return null;
        }
    }
    public class ChromeDetails
    {
        public string HtmlPath { get; set; }
        public string ChromePath { get; set; }
        public string OutputPath { get; set; }
    }
}
