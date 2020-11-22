using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> ManageExperienceAsync(Guid id)
        {
            string experienceEndPoint = "https://localhost:44360/" + "api/reports/" + id;

            ManageExperienceViewModel viewModel = new ManageExperienceViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(experienceEndPoint);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    viewModel = JsonConvert.DeserializeObject<ManageExperienceViewModel>(result);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditExperienceAsync(Guid id)
        {
            string experienceEndPoint = "https://localhost:44360/" + "api/experiences/" + id;
            string categoriesEndPoint = "https://localhost:44360/" + "api/categories";

            EditExperienceViewModel viewModel = new EditExperienceViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage experienceResponse = await client.GetAsync(experienceEndPoint);
                if (experienceResponse.IsSuccessStatusCode)
                {
                    var result = await experienceResponse.Content.ReadAsStringAsync();
                    viewModel.Experience = JsonConvert.DeserializeObject<ExperienceDTO>(result);
                }

                HttpResponseMessage categoriesResponse = await client.GetAsync(categoriesEndPoint);
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var result = await categoriesResponse.Content.ReadAsStringAsync();
                    var categoriesTest = JsonConvert.DeserializeObject<List<CategoryDTO>>(result);
                    viewModel.Categories = categoriesTest.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name });
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditExperienceAsync(Guid id, ExperienceDTO experience)
        {
            string experienceEndPoint = "https://localhost:44360/" + "api/experiences/" + id;

            var content = new StringContent(JsonConvert.SerializeObject(experience), System.Text.Encoding.UTF8, "application/json");

            //ExperienceDTO viewModel = new ExperienceDTO();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsync(experienceEndPoint, content);
                if (response.IsSuccessStatusCode)
                {
                    //var result = await response.Content.ReadAsStringAsync();
                    //viewModel = JsonConvert.DeserializeObject<ExperienceDTO>(result);
                    return View();
                }
            }

            return View();
        }
    }
}
