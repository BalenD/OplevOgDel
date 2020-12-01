using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OplevOgDel.Web.Models;
using OplevOgDel.Web.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Services
{
    public class OplevOgDelService
    {
        private readonly ApiUrls _apiUrls;
        private readonly IHttpContextAccessor _httpContext;

        public HttpClient Client { get; set; }
        public OplevOgDelService(HttpClient client, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContext)
        {
            _apiUrls = apiUrls.Value;
            _httpContext = httpContext;
            Client = client;
            Client.BaseAddress = new Uri(_apiUrls.API);
            var token = _httpContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == UserClaims.AccessToken);
            if (token != null)
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            }
        }

        public async Task<IEnumerable<T>> GetAll<T>(string Path)
        {
            var response = await Client.GetAsync(Path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<T>>();
            }
            return null;
        }
    }
}
