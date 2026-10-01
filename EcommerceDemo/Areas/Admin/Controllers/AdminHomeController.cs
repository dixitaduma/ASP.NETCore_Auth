using Microsoft.AspNetCore.Mvc;
namespace EcommerceDemo.Areas.Admin.Controllers
{
	public class AdminHomeController : Controller
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
