using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace UnitConversionsMauiClient.Utils
{
    internal static class Helpers
    {
        private static readonly MobileHostEnvironment _mobileHostEnvironment = new()
        {
            EnvironmentName = Environments.Production
        };

        public static string GetEndpointRootUrl()
        {
            #if DEBUG
                _mobileHostEnvironment.EnvironmentName = Environments.Development;
            #endif

            string endpointRootUrl = string.Empty;
            if (_mobileHostEnvironment.EnvironmentName == "Production")
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

    internal sealed class MobileHostEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = Environments.Development;
        public string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ContentRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
