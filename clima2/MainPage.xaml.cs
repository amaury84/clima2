using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace clima2
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient Client;    
        public MainPage()
        {
            InitializeComponent();
            Client = new HttpClient();
        }

        private async void ObtenerCLimaBtn(object sender, EventArgs e)
        {
            string city = CityEntry.Text;
            string apikey = "YOUR-API-KEY";
            string apiUrl = "api.openweathermap.org/data/2.5/weather?q="+city+"&APPID="+apikey;
            HttpResponseMessage response = await Client.GetAsync(apiUrl);
            if ( response.IsSuccessStatusCode)
            {
                await DisplayAlert("", "Todo ha salido bien!", "ok");
            }
            string weather = await response.Content.ReadAsStringAsync();
            Clima clima = JsonConvert.DeserializeObject<Clima>(weather);
            
            double txtTemp = clima.main.Temperatura - 273.15;
            WeatherLaber.Text = "Temperatura: "+txtTemp.ToString()+ "°C";
        }
        public class Clima
        {
            [JsonProperty("main")]
            public Main main { get; set; }
        }
        public class Main 
        {
            [JsonProperty("temp")]
            public double Temperatura {  get; set; }
        }
    }
}
