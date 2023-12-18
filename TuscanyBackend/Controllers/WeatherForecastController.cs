using Microsoft.AspNetCore.Mvc;
using TuscanyBackend.DB;
using TuscanyBackend.DB.Models;

namespace TuscanyBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly DbTuscanyContext _context;
       // private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController()
        {
           this._context = new DbTuscanyContext();
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{

        //    this._context.PaymentMethods.Add(new PaymentMethod());

        //    this._context.SaveChanges();

        //    return _context;
        //}
    }
}