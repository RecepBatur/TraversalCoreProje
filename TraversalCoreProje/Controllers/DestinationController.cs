using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Controllers
{
    public class DestinationController : Controller
    {
        DestinationManager destinationManager = new DestinationManager(new EfDestinationDal());
        public IActionResult Index()
        {
            var values = destinationManager.TGetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult DestinaonDetails(int id)
        {
            var values = destinationManager.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult DestinaonDetails(Destination p)
        {
            return View();
        }
    }
}
