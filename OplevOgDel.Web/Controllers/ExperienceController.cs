using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OplevOgDel.Web.Models.Dto;
using OplevOgDel.Web.Models.ViewModel;
using System.Text;
using OplevOgDel.Web.Models;
using Microsoft.Extensions.Options;
using OplevOgDel.Web.Models.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using OplevOgDel.Web.Services;

namespace OplevOgDel.Web.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly ILogger<ExperienceController> _logger;
        private readonly OplevOgDelService _oplevOgDelService;
        private ApiUrls _apiUrls;

        public ExperienceController(IOptions<ApiUrls> apiUrls,
                                    ILogger<ExperienceController> logger,
                                    IConfiguration confirguration,
                                    OplevOgDelService oplevOgDelService)
        {
            _apiUrls = apiUrls.Value;
            _logger = logger;
            _oplevOgDelService = oplevOgDelService;
        }

        [HttpGet("/experiences/{id}")]
        public async Task<IActionResult> ExperienceAsync([FromRoute] Guid id)
        {
            string experiencesEndPoint = _apiUrls.API + _apiUrls.Experiences + $"/{id}";
            string experienceReviewsEndPoint = _apiUrls.API + _apiUrls.Experiences + $"/{id}" + _apiUrls.Reviews;
            //string experienceRatingsEndPoint = _apiUrls.API + _apiUrls.Experiences + $"/{id}" + _apiUrls.Ratings;

            ViewData["ExperienceId"] = RouteData.Values["id"].ToString();

            //TODO:
            //Insert Rating in controller

            ExperienceViewModel viewModel = new ExperienceViewModel();


            var responseExperience = await _oplevOgDelService.Client.GetAsync(experiencesEndPoint);
            if (responseExperience.IsSuccessStatusCode)
            {
                viewModel.Experience = await responseExperience.Content.ReadAsAsync<ExperienceDto>();
            }

            var responseReviews = await _oplevOgDelService.Client.GetAsync(experienceReviewsEndPoint);
            if (responseReviews.IsSuccessStatusCode)
            {
                viewModel.Reviews = await responseReviews.Content.ReadAsAsync<List<ReviewDto>>();
            }

            return View(viewModel);
        }

        [HttpGet("/experiences/createexperience")]
        public async Task<IActionResult> CreateExperience()
        {
            string experiencesCategoriesEndPoint = _apiUrls.API + _apiUrls.Categories;

            CreateOneExperienceViewModel viewModel = new CreateOneExperienceViewModel();

            var responseCategory = await _oplevOgDelService.Client.GetAsync(experiencesCategoriesEndPoint);
            if (responseCategory.IsSuccessStatusCode)
            {
                var result = await responseCategory.Content.ReadAsAsync<List<CategoryForExperienceDto>>();
                viewModel.Categories = result.Select(c => new SelectListItem() { Value = c.Name, Text = c.Name });
            }

            return View(viewModel);
        }

        [HttpPost("/experiences/createexperience")]
        public async Task<IActionResult> CreateExperience(CreateOneExperienceDto experience)
        {
            string experiencesEndPoint = _apiUrls.API + _apiUrls.Experiences;

            var profileId = User.Claims.First(x => x.Type == UserClaims.ProfileId).Value;

            experience.ProfileId = Guid.Parse(profileId);

            var content = new StringContent(JsonConvert.SerializeObject(experience), Encoding.UTF8, "application/json");
            var responseExperience = await _oplevOgDelService.Client.PostAsync(experiencesEndPoint, content);
            if (responseExperience.IsSuccessStatusCode)
            {
                var getId = await responseExperience.Content.ReadAsAsync<ExperienceDto>();
                return Redirect(_apiUrls.Experiences + $"/{getId.Id}");
            }

            return View();
        }

        [HttpPost("/experiences/{id}")]
        public async Task<IActionResult> ExperienceReviewAsync([FromRoute] Guid id, ExperienceViewModel vm)
        {
            string experiencesEndPoint = _apiUrls.API + _apiUrls.Experiences + $"/{id}";
            string experienceReviewsEndPoint = _apiUrls.API + _apiUrls.Experiences + $"/{id}" + _apiUrls.Reviews;

            var profileId = User.Claims.First(x => x.Type == UserClaims.ProfileId).Value;

            vm.CreateOneReview.Description.ToString();
            vm.CreateOneReview.ProfileId = Guid.Parse(profileId);
            vm.CreateOneReview.ExperienceId = id;

            var content = new StringContent(JsonConvert.SerializeObject(vm.CreateOneReview), Encoding.UTF8, "application/json");
            var response = await _oplevOgDelService.Client.PostAsync(experienceReviewsEndPoint, content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Redirect(_apiUrls.Experiences + $"/{id}");
            }

            return Redirect(_apiUrls.Experiences + $"/{id}");
        }

        //[HttpPut("/experiences/{id}")]
        //public async Task<IActionResult> PostExperience([FromRoute] Guid id, Experience experience)
        //{
        //    return View();
        //}

        [HttpDelete("/experiences/{id}")]
        public async Task<IActionResult> DeleteExperience([FromRoute] Guid id)
        {
            //TODO:
            //If delete belongs to user, can delete

            string experiencesEndPoint = _apiUrls.API + _apiUrls.Experiences + $"/{id}";
            string experienceReviewsEndPoint = _apiUrls.API + _apiUrls.Experiences + $"/{id}" + _apiUrls.Reviews;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(experiencesEndPoint);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(experienceReviewsEndPoint);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var experienceReviews = JsonConvert.DeserializeObject<List<ReviewDto>>(result);
                    if (experienceReviews.Count != 0)
                    {
                        foreach (var review in experienceReviews)
                        {
                            HttpResponseMessage deleteResponse = await client.GetAsync(experienceReviewsEndPoint + review.Id);
                            if (deleteResponse.IsSuccessStatusCode)
                            {
                                var deleteResult = await deleteResponse.Content.ReadAsStringAsync();
                            }
                        }
                    }
                }
            }
            return View();
        }
    }
}
