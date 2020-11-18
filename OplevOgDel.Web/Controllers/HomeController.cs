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
            //string categoriesEndPoint = APIAddress + "api/categories";
            //string experiencesEndPoint = APIAddress + "api/experiences";
            string categoriesEndPoint = "https://localhost:44360/" + "api/categories";
            string experiencesEndPoint = "https://localhost:44360/" + "api/experiences";

            HomeViewModel viewModel = new HomeViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage categoriesResponse = await client.GetAsync(categoriesEndPoint);
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var result = await categoriesResponse.Content.ReadAsStringAsync();
                    viewModel.Categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(result);
                }
            
                HttpResponseMessage experiencesResponse = await client.GetAsync(experiencesEndPoint);
                if (experiencesResponse.IsSuccessStatusCode)
                {
                    var result = await experiencesResponse.Content.ReadAsStringAsync();
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
