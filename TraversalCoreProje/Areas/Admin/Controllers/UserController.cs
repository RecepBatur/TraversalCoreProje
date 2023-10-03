using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;

        public UserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public IActionResult Index()
        {
            var values = _appUserService.TGetList();
            return View(values);
        }
        public IActionResult DeleteUser(int id)
        {
            var values = _appUserService.TGetById(id);
            _appUserService.TDelete(values);
            return View(values);
        }
        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            var values = _appUserService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateUser(AppUser appUser)
        {
            _appUserService.TUpdate(appUser);
            return RedirectToAction("Index");
        }
        public IActionResult CommentUser(int id)
        {
            _appUserService.TGetList();
            return View();
        }
        public IActionResult ReservationUser(int id)
        {
            _appUserService.TGetList();
            return View();
        }
    }
}
