using FX.FP.NetCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration//FX.FP.NetCore.Common
{
    public static class CustomConfigurationBuilderExt
    {
        public static IConfigurationBuilder AddCustomConfiguration(this IConfigurationBuilder builder)
        {
            builder.Add(new CustomConfigurationSource());
            return builder;
        }
    }
}
