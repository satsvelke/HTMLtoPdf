Nuget package 
.net framework - https://www.nuget.org/packages/Sats.HTMLtoPdf


Core 3.1 - https://www.nuget.org/packages/Sats.Core.HTMLToPdf


# HTMLtoPdf
Converts HTML content to PDF using chrome executable 


<b>#Note : Requires Chrome executable **</b>




#Usage
    
    
            var url = @"d:\convert.html";
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
        




#For Web Application (or after release)#
                      
                      
                      Go to yourapplication pool → Advanced Settings --> Process Model --> Set Identity to Local


