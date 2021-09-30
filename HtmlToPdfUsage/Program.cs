using Sats.HTMLtoPdf;
using System.IO;

namespace HtmlToPdfUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = @"d:\NuGet Gallery _ Sats.HTMLtoPdf 2.0.0.html";
            var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

            var output = new ChromeOptions().AddOptions(b =>
                                {
                                    b.Headless();
                                    b.DisableGPU();
                                    b.WithoutHeader();
                                }).ToPdf(new ChromeDetails()
                                {
                                    ChromePath = chromePath,
                                    HtmlPath = url,
                                    DeleteOutputFile = true, //optional
                                   // OutputPath = @"d:\print.pdf" // (add if Environment.CurrentDirectory does not have access rights)
                                });


            File.WriteAllBytes(@"d:\print.pdf", output.FileDetails.File);
        }
    }
}
