using BusinessLayer.Abstract.AbstractUow;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraversalCoreProje.Areas.Admin.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(AccountViewModel accountViewModel)
        {
            var valueSender = _accountService.TGetById(accountViewModel.SenderId);
            var valueReceiver = _accountService.TGetById(accountViewModel.ReceiverId);

            valueSender.Balance -= accountViewModel.Amount;
            valueReceiver.Balance += accountViewModel.Amount;


            List<Account> modifiedAccounts = new List<Account>()
            {
                valueSender,
                valueReceiver
            };
            _accountService.TMultiUpdate(modifiedAccounts);


            return View();
        }
    }
}
