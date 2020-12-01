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
using OplevOgDel.Web.Models.Dto;
using OplevOgDel.Web.Models.ViewModel;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace OplevOgDel.Web.Controllers
{
    public class ExperienceController : BaseService
    {
        private readonly ILogger<ExperienceController> _logger;

        public ExperienceController(ILogger<ExperienceController> logger,
                                    IConfiguration confirguration) : base(confirguration)
        {
            _logger = logger;
        }


        [HttpGet("/experiences/{id}")]
        public async Task<IActionResult> ExperienceAsync([FromRoute] Guid id)
        {
            string experiencesEndPoint = APIAddress + $"api/experiences/{id}"; 
            string experienceReviewsEndPoint = APIAddress + $"api/experiences/{id}/reviews/";
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

        [HttpPost("/experiences/{id}")]
        public async Task<IActionResult> ExperienceAsync([FromRoute] Guid id, ExperienceViewModel vm)
        {
            string experiencesEndPoint = APIAddress + $"api/experiences/{id}";
            string experienceReviewsEndPoint = APIAddress + $"api/experiences/{id}/reviews/";

            vm.CreateOneReview.Description.ToString();

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(vm.CreateOneReview), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(experienceReviewsEndPoint, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return Redirect($"/experiences/{id}");
                }
            }
            return Redirect($"/experiences/{id}");
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

            string experiencesEndPoint = APIAddress + $"api/experiences/{id}";
            string experienceReviewsEndPoint = APIAddress + $"api/experiences/{id}/reviews/";

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
