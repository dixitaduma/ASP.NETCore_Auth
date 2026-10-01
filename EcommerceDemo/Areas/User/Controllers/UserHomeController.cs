using Microsoft.AspNetCore.Mvc;
namespace EcommerceDemo.Areas.User.Controllers
{
	[Area("User")]
	public class UserHomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Home()
		{
			return View();
		}
	}
}
