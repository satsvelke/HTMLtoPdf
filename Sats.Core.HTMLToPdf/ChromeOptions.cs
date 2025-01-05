using System;
using System.Collections.Generic;

namespace Sats.Core.HTMLToPdf
{
    public class ChromeOptions
    {
        private List<Options> options { get; } = new List<Options>();

        public List<Options> AddOptions(Action<PolicyBuilder> configurePolicy)
        {
            if (configurePolicy == null)
                throw new ArgumentNullException(nameof(configurePolicy));

            var policyBuilder = new PolicyBuilder();
            configurePolicy(policyBuilder);
            options.Add(policyBuilder.Build());
            return options;
        }
    }
}
