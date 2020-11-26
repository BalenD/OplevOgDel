using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Controllers
{
    public class ReviewController : Controller
    {
        public async Task<IActionResult> DeleteReviewAsync(Guid eId, Guid rId)
        {
            string endPoint = "https://localhost:44360/" + $"api/experiences/{eId}/reviews/{rId}";


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ManageExperience", "Admin", new { id = eId });
                }
            }

            return RedirectToAction("ManageExperience", "Admin", new { id = eId });
        }
    }
}
