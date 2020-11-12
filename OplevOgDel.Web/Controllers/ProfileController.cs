using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OplevOgDel.Web.Controllers.Base;
using OplevOgDel.Web.Models.DTO;
using OplevOgDel.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Controllers
{
    public class ProfileController : BaseService
    {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger,
                                    IConfiguration confirguration) : base(confirguration)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            string endPoint = "https://localhost:44360/" + "api/profiles/9600bf95-bf37-4e6d-aeed-53d84a96a205";

            ProfileViewModel viewModel = new ProfileViewModel();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    viewModel.Profile = JsonConvert.DeserializeObject<ProfileDTO>(result);
                }
            }

            if (viewModel.Profile.ListOfExps.Count != 0)
            {
                viewModel.SelectedListOfExps = viewModel.Profile.ListOfExps[0];
                viewModel.ListOfListOfExps = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(viewModel.Profile.ListOfExps, "Id", "Name");
            }

            return View(viewModel);
        }
    }
}
