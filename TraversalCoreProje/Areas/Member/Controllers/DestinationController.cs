using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    [Route("Member/[controller]/[action]")]
    public class DestinationController : Controller
    {
        DestinationManager destinationManager = new DestinationManager(new EfDestinationDal());
        public IActionResult Index()
        {
            ViewBag.v1 = "Aktif Tur Rotalarımız";
            var values = destinationManager.TGetList();
            return View(values);
        }
        public IActionResult GetCitiesSearchByNames(string searchDestination)
        {
            ViewData["CurrentFilter"] = searchDestination;
            var values = from x in destinationManager.TGetList() select x;
            if (!string.IsNullOrEmpty(searchDestination))
            {
                values = values.Where(y=>y.City.Contains(searchDestination));
            }
            return View(values.ToList());
        }
    }
}
