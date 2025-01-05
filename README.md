
# HTML to PDF Conversion using Chrome Executable

This package allows you to convert HTML files to PDF using the Chrome executable. It leverages Chrome's headless mode to generate PDFs from local or remote HTML files efficiently.

## Features
- **Headless Chrome Mode**: Converts HTML to PDF without launching a full browser window.
- **Custom Output**: Allows custom PDF file output locations.
- **No Header/Footer**: Removes the default header and footer from the PDF.
- **Option for User Directory**: Allows the use of a custom user directory for the conversion process.

---

## Installation
To install this package, follow these steps:

1. **Download**  
   Download the latest `.nupkg` file from the GitHub Releases page, or use the NuGet Package Manager in Visual Studio or the .NET CLI:
   ```bash
   dotnet add package HtmlToPdf --version 2.0.0
   ```

2. **Include the Required Dependencies**  
   - Ensure that Google Chrome is installed on your system.
   - Set the correct paths for the HTML file and Chrome executable in your application.

---

## Usage
Below is an example of how to use the package to convert an HTML file to PDF.

```csharp
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Path to the HTML file you want to convert
        var url = @"d:\3bc00512-c01a-4f6d-a26e-77616ef51807.html";

        // Path to your Chrome executable
        var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

        // Use the ChromeOptions class to set conversion options
        var output = new ChromeOptions().AddOptions(b =>
        {
            // Set Chrome to headless mode (no UI)
            b.Headless();

            // Disable GPU (optional)
            b.DisableGPU();

            // Remove header and footer from the generated PDF
            b.WithoutHeader();
        }).ToPdf(new ChromeDetails()
        {
            ChromePath = chromePath,       // Path to Chrome executable
            HtmlPath = url,                // Path to HTML file
            DeleteOutputFile = true,       // Delete temporary output file after conversion (optional)
            UserDirectoryPath = "d:\\userdir" // Path for Chrome's user data directory (optional)
        });

        // Save the generated PDF to the specified path
        File.WriteAllBytes(@"d:\print.pdf", output.FileDetails.File);
    }
}
```

---

## Parameters Explained:
- **HtmlPath**: The file path of the HTML document you want to convert to PDF.  
  *Example*: `"d:\\3bc00512-c01a-4f6d-a26e-77616ef51807.html"`

- **ChromePath**: The file path to your Google Chrome executable.  
  *Example*: `"C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"`

- **DeleteOutputFile**: A boolean flag to specify whether the temporary output file (during the conversion process) should be deleted.  
  *Default*: `true`

- **UserDirectoryPath**: Optional path where Chrome will store user data during the conversion process (used for handling cookies, cache, etc.).  
  *Example*: `"d:\\userdir"`

---

## Customization Options:
- **Headless Mode**: The `Headless()` option allows Chrome to run in headless mode, meaning no graphical user interface (GUI) is shown.

- **Disable GPU**: The `DisableGPU()` option can be used to disable the GPU hardware acceleration.

- **Remove Header/Footer**: The `WithoutHeader()` option removes any page numbers, date, or URL that Chrome normally adds to the PDF.

---

## Example Output
- The HTML content at the specified path will be converted to a PDF and saved at the path `d:\print.pdf`.
- Any headers and footers (like page numbers and dates) will be removed.

---

## Common Issues and Troubleshooting

### **Issue: No PDF Generated**
- Ensure Chrome is installed and the path is correctly specified in the `ChromePath` property.
- Make sure the HTML file path is correct and accessible.

### **Issue: Header/Footer Still Appears**
- Ensure that you are using the `WithoutHeader()` option.  
- If the problem persists, check if any default settings in Chrome override this option.

### **Issue: Permissions Error**
- Ensure the paths you are providing for `HtmlPath` and output files (`d:\print.pdf`) are accessible and writable.

---

## License
This package is licensed under the MIT License.

---

## Contributors
- **satsvelke** (author)
