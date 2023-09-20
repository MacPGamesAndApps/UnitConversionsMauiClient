using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitConversionsMauiClient.Utils
{
    internal static class Helpers
    {
        public static string GetEndpointRootUrl()
        {
            string endpointRootUrl = string.Empty;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production")
            {
                endpointRootUrl = Constants.ConversionServiceEndPoints.PROD_UNIT_CONVERSION_ENDPOINT;
            }
            else
            {
                endpointRootUrl = Constants.ConversionServiceEndPoints.DEV_UNIT_CONVERSION_ENDPOINT;
            }
            return endpointRootUrl;
        }
    }
}
