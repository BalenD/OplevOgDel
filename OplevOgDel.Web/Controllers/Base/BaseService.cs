using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OplevOgDel.Web.Controllers.Base
{
    public class BaseService : Controller
    {
        protected readonly string APIAddress;

        public BaseService(IConfiguration configuration)
        {
            APIAddress = configuration["API"];
        }
    }
}
