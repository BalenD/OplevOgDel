using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OplevOgDel.Web.Controllers.Base;
using OplevOgDel.Web.Models;

namespace OplevOgDel.Web.Controllers
{
    public class ExperienceController : BaseService
    {
        private readonly ILogger<HomeController> _logger;

        public ExperienceController(ILogger<HomeController> logger,
                                    IConfiguration confirguration) : base(confirguration)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Experiences()
        {
            string endPoint = APIAddress + "api/experiences";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
            }
            return View();
        }

        [HttpGet("/experiences/{id}")]
        public async Task<IActionResult> GetExperience([FromRoute] Guid id)
        {
            string endPoint = APIAddress + "api/experiences/" + id;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
            }
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> PostExperience(Experience experience)
        //{
        //    return View();
        //}

        //[HttpPut("/experiences/{id}")]
        //public async Task<IActionResult> PostExperience([FromRoute] Guid id, Experience experience)
        //{
        //    return View();
        //}

        [HttpDelete("/experiences/{id}")]
        public async Task<IActionResult> DeleteExperience([FromRoute] Guid id)
        {
            string endPoint = APIAddress + "api/experiences" + id;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
            }
            return View();
        }
    }
}
