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
using OplevOgDel.Web.Models.Dto;
using OplevOgDel.Web.Models.ViewModel;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using OplevOgDel.Web.Models;
using Microsoft.Extensions.Options;
using OplevOgDel.Web.Models.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OplevOgDel.Web.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly ILogger<ExperienceController> _logger;
        private ApiUrls _apiUrls;

        public ExperienceController(IOptions<ApiUrls> apiUrls,
                                    ILogger<ExperienceController> logger,
                                    IConfiguration confirguration)
        {
            _apiUrls = apiUrls.Value;
            _logger = logger;
        }

        [HttpGet("/experiences/{id}")]
        public async Task<IActionResult> ExperienceAsync([FromRoute] Guid id)
        {
            string experiencesEndPoint = _apiUrls.API + _apiUrls.Experiences + $"/{id}";
            string experienceReviewsEndPoint = _apiUrls.API + _apiUrls.Experiences + $"/{id}" + _apiUrls.Reviews;
            //string experienceRatingsEndPoint = APIAddress + $"api/experiences/{id}/ratings/";

            ViewData["ExperienceId"] = RouteData.Values["id"].ToString();

            //TODO:
            //Insert Rating in controller

            ExperienceViewModel viewModel = new ExperienceViewModel();

            using (HttpClient client = new HttpClient())
            {
                //string accessToken = await HttpContext.GetTokenAsync("access_token");
                //var request = new HttpRequestMessage(HttpMethod.Get, experiencesEndPoint);
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = await client.GetAsync(experiencesEndPoint);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    viewModel.Experience = JsonConvert.DeserializeObject<ExperienceDto>(result);
                }
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(experienceReviewsEndPoint);
                if (response.IsSuccessStatusCode) 
                {
                    var result = await response.Content.ReadAsStringAsync();
                    viewModel.Reviews = JsonConvert.DeserializeObject<List<ReviewDto>>(result);
                }
            }
            return View(viewModel);
        }

        [HttpGet("/experiences/createexperience")]
        public async Task<IActionResult> CreateExperience()
        {
            CreateOneExperienceViewModel viewModel = new CreateOneExperienceViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage categoriesResponse = await client.GetAsync(_apiUrls.API + _apiUrls.Categories);
                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var result = await categoriesResponse.Content.ReadAsStringAsync();
                    var categoriesTest = JsonConvert.DeserializeObject<List<CategoryDto>>(result);
                    viewModel.Categories = categoriesTest.Select(c => new SelectListItem() { Value = c.Name, Text = c.Name });
                }
            }
            return View(viewModel);
        }

        [HttpPost("/experiences/createexperience")]
        public async Task<IActionResult> CreateExperience(CreateOneExperienceDto experience)
        {
            //if (experience == null || experience == null)
            //{
            //    ViewBag.Message = "Udfyld venligst begge felter!";
            //    return View();
            //}

            var profileId = User.Claims.First(x => x.Type == UserClaims.ProfileId).Value;

            experience.ProfileId = Guid.Parse(profileId);
            

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(experience), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_apiUrls.API + _apiUrls.Experiences, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var getId = JsonConvert.DeserializeObject<ExperienceDto>(result);
                    return Redirect(_apiUrls.Experiences + $"/{getId.Id}");
                }
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

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(vm.CreateOneReview), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(experienceReviewsEndPoint, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Redirect(_apiUrls.Experiences + $"/{id}");
                }
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
