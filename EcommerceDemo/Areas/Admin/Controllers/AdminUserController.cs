using Microsoft.AspNetCore.Mvc;

namespace EcommerceDemo.Areas.Admin.Controllers
{
	public class AdminUserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
