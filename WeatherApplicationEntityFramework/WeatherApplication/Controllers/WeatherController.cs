using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApplication.Services;
using Ninject;
using WeatherApplication.Models;
using System.Data.Entity;

namespace WeatherApplication.Controllers
{
    public class WeatherController : Controller
    {
        private IWeatherService service;
        WeatherDataContext bd = new WeatherDataContext();
        // GET: Weather
        public WeatherController(IWeatherService _service)
        {
            service = _service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetWeather(string city,int term)
        {
            var result = service.GetWeatherForecast(city);
            for (int i = 0; i < term; i++)
            {
                var data = DateTime.Now;
                data=data.AddDays(i);
                int day = result.list[i].temp.DayInCelsius();
                int night = result.list[i].temp.NightInCelsius();
                string description = result.list[i].weather[0].description;
                ArchieveWeatherData wd = new ArchieveWeatherData(result.city.name, data.Date.ToString("dd/MM/yyyy"), day, night, description);
                bd.weatherDataRecords.Add(wd);
            }
            bd.SaveChanges();
            ViewBag.days = term;
            return View(result);
        }

        public ActionResult GetWeatherHistory()
        {
            ViewBag.records = bd.weatherDataRecords.ToList();
            return View();
        }

    }
}