using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.ViewComponents.Comment
{
    public class _CommentList : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
