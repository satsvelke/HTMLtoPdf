namespace Sats.Core.HTMLToPdf
{
    internal static class Constants
    {
        public static readonly string WithoutHeader = "--print-to-pdf-no-header --no-pdf-header-footer";
        public static readonly string WithHeader = string.Empty;
        public static readonly string Headless = "--headless";
        public static readonly string DisableGPU = "--disable-gpu";
        public static readonly string RemoteDebugging = "--remote-debugging-port";
    }
}
