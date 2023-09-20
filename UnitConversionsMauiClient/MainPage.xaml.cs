using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UnitConversionsMauiClient.Models;
using UnitConversionsMauiClient.Utils;

namespace UnitConversionsMauiClient;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		this.ConversionType.ItemsSource = GetConversionTypes();
	}

	private void OnConvertClicked(object sender, EventArgs e)
	{
        string? results = string.Empty;
        try
        {
            double? convertedValue = null;
            double fromValue;
            Double.TryParse(this.ValueToConvert.Text, out fromValue);
            UnitConversionData conversionData = new UnitConversionData { ValueFrom = fromValue, ConversionType = (byte)this.ConversionType.SelectedIndex };

            using (HttpClient httpClient = new HttpClient())
            {
                string postContent = JsonConvert.SerializeObject(conversionData);
                StringContent stringContent = new StringContent(postContent, Encoding.UTF8, "application/json");
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage httpResponseMessage = httpClient.PostAsync(Helpers.GetEndpointRootUrl() + "api/unitconversions/convert", stringContent).Result;
                double responseValue = 0;
                if (Double.TryParse(httpResponseMessage.Content.ReadAsStringAsync().Result, out responseValue))
                {
                    convertedValue = responseValue;
                }
            }
            results = convertedValue.ToString();
        }
        catch (HttpRequestException ex)
        {
            results = Constants.Messages.ERROR_MICROSERVICE_CONNECTION_FAILED;
        }
        catch (Exception ex)
        {
            results = ex.Message;
        }
        this.ConvertedValue.Text = results;
    }

    private static List<string> GetConversionTypes()
    {
        List<string> conversionTypeList = new List<string>();

        string conversionTypeListJason = string.Empty;

        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(Helpers.GetEndpointRootUrl() + "api/unitconversions/gettypes").Result;
            conversionTypeListJason = httpResponseMessage.Content.ReadAsStringAsync().Result;
        }

        conversionTypeList = JsonConvert.DeserializeObject<List<ConversionTypeInfo>>(conversionTypeListJason)
            .OrderBy(ctl => ctl.ConversionType)
            .Select(ctl => ctl.ConversionName)
            .ToList<string>();

        return conversionTypeList;
    }
}

