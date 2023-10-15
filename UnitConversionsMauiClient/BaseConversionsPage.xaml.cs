using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UnitConversionsMauiClient.Models;
using UnitConversionsMauiClient.Utils;

namespace UnitConversionsMauiClient;

public partial class BaseConversionsPage : ContentPage
{
	public BaseConversionsPage()
	{
		InitializeComponent();
        this.ConversionType.ItemsSource = Helpers.GetConversionTypes(FeatureFlags.BaseConversions);
    }

    private void OnConvertClicked(object sender, EventArgs e)
    {
        string results = string.Empty;
        try
        {
            BaseConversionData conversionData = new BaseConversionData { ValueFrom = this.ValueToConvert.Text, ConversionType = (byte)this.ConversionType.SelectedIndex };

            using (HttpClient httpClient = new HttpClient())
            {
                string postContent = JsonConvert.SerializeObject(conversionData);
                StringContent stringContent = new StringContent(postContent, Encoding.UTF8, "application/json");
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage httpResponseMessage = httpClient.PostAsync(Helpers.GetEndpointRootUrl(FeatureFlags.BaseConversions) + "convert", stringContent).Result;
                results = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
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

    private async void OnNavigationClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//UnitConversionsPage");
    }
}