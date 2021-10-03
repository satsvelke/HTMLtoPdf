Nuget package 
.net framework - https://www.nuget.org/packages/Sats.HTMLtoPdf
Core 3.1 - https://www.nuget.org/packages/Sats.Core.HTMLToPdf


# HTMLtoPdf
Converts HTML content to PDF using chrome executable 


<b>#Note : Requires Chrome executable **</b>


#Nuget Link  - https://www.nuget.org/packages/Sats.HTMLtoPdf 



#Usage

           
                  var url = @"d:\Vaccination.html";
                  var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                  
                  // returns byte array of file 
                  var pdf = new ChromeOptions().AddOptions(b =>
                                              {
                                                  b.Headless();
                                                  b.DisableGPU();
                                                  b.WithoutHeader();

                                              }).Pdf(new ChromeDetails() { ChromePath = chromePath, HtmlPath = url });

                   File.WriteAllBytes(@"d:\print.pdf", pdf);                          
                    
                    
  #Version 2.1 Usage 
    
    
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
        
output.FileDetails.File will have byte array of created pdf, output.ProcessDetails will have output details like errors


