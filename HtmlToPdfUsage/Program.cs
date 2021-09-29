using Sats.HTMLtoPdf;
using System.IO;

namespace HtmlToPdfUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = @"d:\Vaccination.html";
            var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

            var pdf = new ChromeOptions().AddOptions(b =>
                                {
                                    b.Headless();
                                    b.DisableGPU();
                                    b.WithoutHeader();

                                }).Pdf(new ChromeDetails() { ChromePath = chromePath, HtmlPath = url });

            File.WriteAllBytes(@"d:\print.pdf", pdf);
        }
    }
}
