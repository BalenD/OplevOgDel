using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OplevOgDel.Web.Models;
using OplevOgDel.Web.Models.Configuration;
using OplevOgDel.Web.Models.Dto;
using OplevOgDel.Web.Models.ViewModel;

namespace OplevOgDel.Web.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private ApiUrls _apiUrls;

        public AdminController(IOptions<ApiUrls> apiUrls)
        {
            _apiUrls = apiUrls.Value;
        }
        public async Task<IActionResult> ReportsAsync()
        {
            ReportsViewModel viewModel = new ReportsViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_apiUrls.API + _apiUrls.Reports);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    viewModel.Experiences = JsonConvert.DeserializeObject<List<ExperienceDto>>(result);
                }
            }

            return View(viewModel);
        }

        public async Task<IActionResult> ManageExperienceAsync(Guid id)
        {
            ManageExperienceViewModel viewModel = new ManageExperienceViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_apiUrls.API + _apiUrls.Reports + $"/{id}");
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
            EditExperienceViewModel viewModel = new EditExperienceViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage experienceResponse = await client.GetAsync(_apiUrls.API + _apiUrls.Experiences + $"/{id}");
                if (experienceResponse.IsSuccessStatusCode)
                {
                    var result = await experienceResponse.Content.ReadAsStringAsync();
                    viewModel.Experience = JsonConvert.DeserializeObject<ExperienceDto>(result);
                }

                HttpResponseMessage categoriesResponse = await client.GetAsync(_apiUrls.API + _apiUrls.Categories);
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var result = await categoriesResponse.Content.ReadAsStringAsync();
                    var categoriesTest = JsonConvert.DeserializeObject<List<CategoryDto>>(result);
                    viewModel.Categories = categoriesTest.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name });
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditExperienceAsync(Guid id, ExperienceDto experience)
        {
            var content = new StringContent(JsonConvert.SerializeObject(experience), System.Text.Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsync(_apiUrls.API + _apiUrls.Experiences + $"/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ManageExperience", new { id });
                }
            }

            return RedirectToAction("EditExperience", new { id });

        }
    }
}
