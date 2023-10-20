using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.ViewComponents.Destination
{
    public class _RandomGuide : ViewComponent
    {
        private readonly IGuideService _guideService;

        public _RandomGuide(IGuideService guideService)
        {
            _guideService = guideService;
        }
        public IViewComponentResult Invoke()
        {
            var values = _guideService.TGetById(5);
            return View(values);
        }
    }
}
