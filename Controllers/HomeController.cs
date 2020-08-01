using PostGISWeb.PostgreSQL;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostGISWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace PostGISWeb.Controllers
{
    public class HomeController : Controller
    {
        static PostGISContext context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            context = new PostGISContext(_configuration);
            _logger = logger;

        }

        public IActionResult Index()
        {
            List<GisMap> result = context.GisMap.
                         FromSqlRaw("SELECT gid, ST_AsGeoJSON(geom) as geoJSON from gis Where gid = " +
                         context.GisObjects.FirstOrDefault().gid.ToString()).ToList();
            return View(result);
        }
        public IActionResult Map()
        {
            return View();
        }
        public ContentResult APIGeoJson()
        {
            GisMap result = context.GisMap.
                        FromSqlRaw("SELECT gid, ST_AsGeoJSON(geom) as geoJSON from gis Where gid = {0}",
                        context.GisObjects.FirstOrDefault().gid)
                        .AsEnumerable().FirstOrDefault();

            return Content(result.geoJSON);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
