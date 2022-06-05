using filters.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace filters.Controllers
{
    public class HomeController : Controller
    {
        [TypeFilter(typeof(DiagnosticsFilter))]
        [ServiceFilter(typeof(TimeFilter))]
        [Profile]
        public ViewResult Index() => View("Message", "This is the Index action on the Home controller");
    }
}
