using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OplevOgDel.Web.Controllers.Base;
using OplevOgDel.Web.Models;
using OplevOgDel.Web.Models.DTO;
using OplevOgDel.Web.Models.ViewModel;

namespace OplevOgDel.Web.Controllers
{
    public class HomeController : BaseService
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
                                    IConfiguration confirguration) : base(confirguration)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            string categoriesEndPoint = "https://localhost:44360/" + "api/categories";
            string experiencesEndPoint = "https://localhost:44360/" + "api/experiences";
            //string endPoint = "https://localhost:44360/" + "api/experiences";
            //string endPoint = APIAddress + "api/experiences";

            HomeViewModel viewModel = new HomeViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(categoriesEndPoint);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    viewModel.Categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(result);
                }
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(experiencesEndPoint);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    viewModel.Experiences = JsonConvert.DeserializeObject<List<ExperienceDTO>>(result);
                }
            }
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
