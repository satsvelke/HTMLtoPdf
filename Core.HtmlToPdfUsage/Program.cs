using Sats.Core.HTMLToPdf;
using System.IO;

namespace Core.HtmlToPdfUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = @"d:\3bc00512-c01a-4f6d-a26e-77616ef51807.html";
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
                DeleteOutputFile = true,
                UserDirectoryPath = "d:\\userdir"
            });

            File.WriteAllBytes(@"d:\print.pdf", output.FileDetails.File);
        }
    }
}
