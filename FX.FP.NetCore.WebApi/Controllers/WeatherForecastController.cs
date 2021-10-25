using FX.FP.NetCore.Interface;
using FX.FP.NetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FX.FP.NetCore.Common.DotNetData;

namespace FX.FP.NetCore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBlogPost<BlogPost> _blogPost;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBlogPost<BlogPost> blogPost)
        {
            _logger = logger;
            _blogPost = blogPost;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public void Insert(string title, string content)
        {
            _blogPost.Insert(title, content);
        }

        [HttpGet("GetList")]
        public List<BlogPost> GetList()
        {
            return (_blogPost.GetList());
        }

        [HttpGet("GetPageList")]
        public JsonResult GetPageList(string createDate)
        {
            DataTable table = _blogPost.GetPageList(Convert.ToDateTime(createDate));
            return new JsonResult(table.ToJson());
        }
    }
}
