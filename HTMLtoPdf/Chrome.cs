using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

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


        public static Output ToPdf(this List<Options> options, ChromeDetails chromeDetails)
        {
            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();
            var exitCode = string.Empty;
            int timeout = 100;

            if (options.Count == 0)
                return null;


            var chromeOptions = options.Select(c => c.ChromeOptions).FirstOrDefault();


            if (chromeOptions.Count() == 0)
                return null;

            var arguments = string.Join(" ", chromeOptions.Select(c => c.ToString()));


            var output = !string.IsNullOrWhiteSpace(chromeDetails.OutputPath) ? chromeDetails.OutputPath :
                Path.Combine(Environment.CurrentDirectory, $"printout_{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()}.pdf");

            using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
            using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = chromeDetails.ChromePath;
                    process.StartInfo.Arguments = $"{arguments} --print-to-pdf={output} {chromeDetails.HtmlPath}";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;

                    try
                    {
                        process.OutputDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                                outputWaitHandle.Set();
                            else
                                outputBuilder.AppendLine(e.Data);
                        };
                        process.ErrorDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                                errorWaitHandle.Set();
                            else
                                errorBuilder.AppendLine(e.Data);
                        };

                        process.Start();

                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();

                        if (process.WaitForExit(timeout))
                            exitCode = Convert.ToString(process.ExitCode);
                        else
                            // timed out

                            output = outputBuilder.ToString();
                    }
                    finally
                    {
                        outputWaitHandle.WaitOne(timeout);
                        errorWaitHandle.WaitOne(timeout);
                    }
                }
            }

            if (File.Exists(output) && chromeDetails.DeleteOutputFile)
            {
                var file = File.ReadAllBytes(output);
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
            else return null;
        }

    }


    public class ChromeDetails
    {
        public string HtmlPath { get; set; }
        public string ChromePath { get; set; }
        public string OutputPath { get; set; }
        public bool DeleteOutputFile { get; set; } = true;
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
