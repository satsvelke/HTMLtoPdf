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
                    
