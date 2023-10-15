using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UnitConversionsMauiClient.Models;

namespace UnitConversionsMauiClient.Utils
{
    internal static class Helpers
    {
        private static readonly MobileHostEnvironment _mobileHostEnvironment = new()
        {
            EnvironmentName = Environments.Production
        };

        public static List<string> GetConversionTypes(FeatureFlags feature)
        {
            List<string> conversionTypeList = new List<string>();

            string conversionTypeListJason = string.Empty;

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = httpClient.GetAsync(Helpers.GetEndpointRootUrl(feature) + "gettypes").Result;
                conversionTypeListJason = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }

            conversionTypeList = JsonConvert.DeserializeObject<List<ConversionTypeInfo>>(conversionTypeListJason)
                .OrderBy(ctl => ctl.ConversionType)
                .Select(ctl => ctl.ConversionName)
                .ToList<string>();

            return conversionTypeList;
        }

        public static string GetEndpointRootUrl(FeatureFlags feature)
        {
            #if DEBUG
                _mobileHostEnvironment.EnvironmentName = Environments.Development;
            #endif

            string endpointRootUrl = string.Empty;
            switch (feature)
            {
                case FeatureFlags.UnitConversions:
                    if (_mobileHostEnvironment.EnvironmentName == "Production")
                    {
                        endpointRootUrl = Constants.ConversionServiceEndPoints.PROD_UNIT_CONVERSION_ENDPOINT;
                    }
                    else
                    {
                        endpointRootUrl = Constants.ConversionServiceEndPoints.DEV_UNIT_CONVERSION_ENDPOINT;
                    }
                    break;
                case FeatureFlags.BaseConversions:
                    if (_mobileHostEnvironment.EnvironmentName == "Production")
                    {
                        endpointRootUrl = Constants.ConversionServiceEndPoints.PROD_BASE_CONVERSION_ENDPOINT;
                    }
                    else
                    {
                        endpointRootUrl = Constants.ConversionServiceEndPoints.DEV_BASE_CONVERSION_ENDPOINT;
                    }
                    break;
                default:
                    break;
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
