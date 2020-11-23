using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OplevOgDel.Web.Controllers
{
    public class ReportController : Controller
    {
        public async Task<IActionResult> DeleteReportsForExperienceAsync(Guid id)
        {
            string endPoint = "https://localhost:44360/" + "api/reports/experience/" + id;


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ManageExperience", "Admin", new { id });
                }
            }

            return RedirectToAction("ManageExperience", "Admin", new { id });
        }

        public async Task<IActionResult> DeleteReportsForReviewAsync(Guid id)
        {
            string endPoint = "https://localhost:44360/" + "api/reports/review/" + id;


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ManageExperience", "Admin", new { id });
                }
            }

            return RedirectToAction("ManageExperience", "Admin", new { id });
        }
    }
}
