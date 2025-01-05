using System.IO;
using System;

namespace Sats.Core.HTMLToPdf
{
    public class PolicyBuilder
    {
        private readonly Options options = new Options();

        public PolicyBuilder WithoutHeader()
        {
            if (!options.ChromeOptions.Contains(Constants.WithoutHeader))
                options.ChromeOptions.Add(Constants.WithoutHeader);

            return this;
        }

        public PolicyBuilder WithHeader()
        {
            if (!options.ChromeOptions.Contains(Constants.WithHeader))
                options.ChromeOptions.Add(Constants.WithHeader);

            return this;
        }

        public PolicyBuilder Headless()
        {
            if (!options.ChromeOptions.Contains(Constants.Headless))
                options.ChromeOptions.Add(Constants.Headless);

            return this;
        }

        public PolicyBuilder DisableGPU()
        {
            if (!options.ChromeOptions.Contains(Constants.DisableGPU))
                options.ChromeOptions.Add(Constants.DisableGPU);

            return this;
        }

        public PolicyBuilder RemoteDebugging(int port)
        {
            if (!options.ChromeOptions.Contains(Constants.RemoteDebugging))
                options.ChromeOptions.Add($"{Constants.RemoteDebugging}={port}");

            return this;
        }

        public Options Build() => options;
    }
}
