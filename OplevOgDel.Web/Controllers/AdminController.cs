using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OplevOgDel.Web.Models.DTO;
using OplevOgDel.Web.Models.ViewModel;

namespace OplevOgDel.Web.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> ReportsAsync()
        {
            string reportsEndPoint = "https://localhost:44360/" + "api/reports";

            ReportsViewModel viewModel = new ReportsViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(reportsEndPoint);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    viewModel.Experiences = JsonConvert.DeserializeObject<List<ExperienceDTO>>(result);
                }
            }

            return View(viewModel);
        }
    }
}
