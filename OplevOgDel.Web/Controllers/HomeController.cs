using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OplevOgDel.Web.Models;
using OplevOgDel.Web.Models.Configuration;
using OplevOgDel.Web.Models.Dto;
using OplevOgDel.Web.Models.ViewModel;

namespace OplevOgDel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiUrls _apiUrls;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IOptions<ApiUrls> apiUrls)
        {
            _apiUrls = apiUrls.Value;
            _logger = logger;
        }

        public async Task<IActionResult> Index([FromQuery] string category, [FromQuery] string city)
        {
            string categoriesEndPoint = _apiUrls.API + _apiUrls.Categories;
            string experiencesEndPoint = _apiUrls.API + _apiUrls.Experiences;

            HomeViewModel viewModel = new HomeViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage categoriesResponse = await client.GetAsync(categoriesEndPoint);
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    viewModel.Categories = await categoriesResponse.Content.ReadAsAsync<List<CategoryDto>>();
                }
            
                HttpResponseMessage experiencesResponse = await client.GetAsync(experiencesEndPoint);
                if (experiencesResponse.IsSuccessStatusCode)
                {
                    viewModel.Experiences = await experiencesResponse.Content.ReadAsAsync<List<ExperienceDto>>();
                    
                }
            }
            if (category != null)
            {
                viewModel.Categories.Where(x => x.Name == category).FirstOrDefault().Clicked = true;
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
