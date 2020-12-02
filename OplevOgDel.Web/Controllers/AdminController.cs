using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using OplevOgDel.Web.Models;
using OplevOgDel.Web.Models.Configuration;
using OplevOgDel.Web.Models.Dto;
using OplevOgDel.Web.Models.ViewModel;
using OplevOgDel.Web.Services;

namespace OplevOgDel.Web.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private readonly ApiUrls _apiUrls;
        private readonly OplevOgDelService _oplevOgDelService;

        public AdminController(IOptions<ApiUrls> apiUrls, OplevOgDelService oplevOgDelService)
        {
            _apiUrls = apiUrls.Value;
            _oplevOgDelService = oplevOgDelService;
        }
        public async Task<IActionResult> ReportsAsync()
        {
            // TODO: SLET
            var roleAdmin = User.FindFirst(ClaimTypes.Role).Value;
            var profileId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var role = User.Claims.First(x => x.Type == ClaimTypes.Role).Value;
            var token = User.FindFirst("access_token").Value;
            // TODO SLUT.

            ReportsViewModel viewModel = new ReportsViewModel();

            HttpResponseMessage response = await _oplevOgDelService.Client.GetAsync(_apiUrls.Reports);
            if (response.IsSuccessStatusCode)
            {
                viewModel.Experiences = await response.Content.ReadAsAsync<List<ExperienceDto>>();
            }

            return View(viewModel);
        }

        public async Task<IActionResult> ManageExperienceAsync(Guid id)
        {
            ManageExperienceViewModel viewModel = new ManageExperienceViewModel();

            HttpResponseMessage response = await _oplevOgDelService.Client.GetAsync(_apiUrls.Reports + $"/{id}");
            if (response.IsSuccessStatusCode)
            {
                viewModel = await response.Content.ReadAsAsync<ManageExperienceViewModel>();
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditExperienceAsync(Guid id)
        {
            EditExperienceViewModel viewModel = new EditExperienceViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage experienceResponse = await _oplevOgDelService.Client.GetAsync(_apiUrls.Experiences + $"/{id}");
                if (experienceResponse.IsSuccessStatusCode)
                {
                    viewModel.Experience = await experienceResponse.Content.ReadAsAsync<ExperienceDto>();
                }

                HttpResponseMessage categoriesResponse = await _oplevOgDelService.Client.GetAsync(_apiUrls.Categories);
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var categories = await categoriesResponse.Content.ReadAsAsync<List<CategoryDto>>();
                    viewModel.Categories = categories.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name });
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditExperienceAsync(Guid id, ExperienceDto experience)
        {
            var content = new StringContent(JsonConvert.SerializeObject(experience), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _oplevOgDelService.Client.PutAsync(_apiUrls.Experiences + $"/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ManageExperience", new { id });
            }

            return RedirectToAction("EditExperience", new { id });

        }
    }
}
