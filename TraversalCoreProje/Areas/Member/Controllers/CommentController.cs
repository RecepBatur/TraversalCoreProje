using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProje.Areas.Member.Controllers
{
    [Area("Member")]
    [AllowAnonymous]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            ViewBag.v1 = "Yorumlarım";
            var values = _commentService.GetListCommentsWithDestination();
            return View(values);
        }
    }
}
