namespace UnitConversionsMauiClient.Utils
{
    internal static class Constants
    {
        public static class Messages
        {
            public static readonly string ERROR_MICROSERVICE_CONNECTION_FAILED = "Failed to connect to Microservice. The Microservice is not responding. Please, make sure it is running and working properly.";
        }

        public static class ConversionServiceEndPoints
        {
            public static readonly string DEV_UNIT_CONVERSION_ENDPOINT = "http://localhost:60937/api/unitconversions/";
            public static readonly string PROD_UNIT_CONVERSION_ENDPOINT = "https://macpunitconversionsmicroservice.azurewebsites.net/api/unitconversions/";
            public static readonly string DEV_BASE_CONVERSION_ENDPOINT = "http://localhost:60947/api/baseconversions/";
            public static readonly string PROD_BASE_CONVERSION_ENDPOINT = "https://macpbaseconversionsmicroservice.azurewebsites.net/api/baseconversions/";
        }
    }
}
