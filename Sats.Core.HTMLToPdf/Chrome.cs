using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Sats.Core.HTMLToPdf
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


        public static Output ToPdf(this List<Options> options, ChromeDetails chromeDetails)
        {
            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();
            var exitCode = string.Empty;

            if (options.Count == 0)
                return null;


            var chromeOptions = options.Select(c => c.ChromeOptions).FirstOrDefault();


            if (chromeOptions.Count() == 0)
                return null;

            var arguments = string.Join(" ", chromeOptions.Select(c => c.ToString()));


            string tempUserDirectory = string.Empty;

            if (string.IsNullOrWhiteSpace(chromeDetails.UserDirectoryPath))
                tempUserDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            else tempUserDirectory = chromeDetails.UserDirectoryPath;

            Directory.CreateDirectory(tempUserDirectory);


            var output = !string.IsNullOrWhiteSpace(chromeDetails.OutputPath) ? chromeDetails.OutputPath :
                Path.Combine(Environment.CurrentDirectory, $"printout_{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()}.pdf");

            Process p = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = chromeDetails.ChromePath,
                    Arguments = $"{arguments} --disable-print-preview --user-data-dir={tempUserDirectory} --print-to-pdf={output} {chromeDetails.HtmlPath}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true
                }
            };
            p.Start();

            StreamWriter streamWriter = p.StandardInput;
            StreamReader outputReader = p.StandardOutput;
            StreamReader errorReader = p.StandardError;
            while (!outputReader.EndOfStream)
                outputBuilder.Append(outputReader.ReadLine());

            while (!errorReader.EndOfStream)
                errorBuilder.Append(errorReader.ReadLine());


            if (Directory.Exists(tempUserDirectory)) Directory.Delete(tempUserDirectory, true);

            if (File.Exists(output))
            {
                var file = File.ReadAllBytes(output);

                if (File.Exists(output) && chromeDetails.DeleteOutputFile)
                    File.Delete(output);

                return new Output()
                {
                    FileDetails = new FileDetails()
                    {
                        File = file,
                        FileSize = file.Length
                    },
                    ProcessDetails = new ProcessDetails()
                    {
                        ExitCode = exitCode,
                        Output = Convert.ToString(outputBuilder),
                        Error = Convert.ToString(errorBuilder),
                    }
                };
            }
            else return new Output()
            {
                ProcessDetails = new ProcessDetails()
                {
                    ExitCode = exitCode,
                    Output = Convert.ToString(outputBuilder),
                    Error = Convert.ToString(errorBuilder),
                }
            };
        }

    }


    public class ChromeDetails
    {
        public string HtmlPath { get; set; }
        public string ChromePath { get; set; }
        public string OutputPath { get; set; }
        public bool DeleteOutputFile { get; set; } = true;
        public string? UserDirectoryPath { get; set; }
    }

    public class Output
    {
        public FileDetails FileDetails { get; set; }
        public ProcessDetails ProcessDetails { get; set; }
    }

    public class FileDetails
    {
        public byte[] File { get; set; }
        public long FileSize { get; set; }
    }

    public class ProcessDetails
    {
        public string ExitCode { get; set; }
        public string Output { get; set; }
        public string Error { get; set; }
    }
}
