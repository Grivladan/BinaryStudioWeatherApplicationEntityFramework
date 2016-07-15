using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Net;
using WeatherApplication.Models;
using Newtonsoft.Json;

namespace WeatherApplication.Services
{
    public interface IWeatherService
    {
        WeatherData.RootObject GetWeatherForecast(string city);
    }

    public class WeatherService: IWeatherService
    {

        public WeatherData.RootObject GetWeatherForecast(string city)
        {
            WebClient wc = new WebClient();
            string url = "http://api.openweathermap.org/data/2.5/forecast/daily?q=" + city + "&mode=json&APPID=885b91841cbfc4e0644d236d80d78621";
            var json = wc.DownloadString(url);
            var obj = JsonConvert.DeserializeObject<WeatherData.RootObject>(json);
            return obj;
        }
    }
}