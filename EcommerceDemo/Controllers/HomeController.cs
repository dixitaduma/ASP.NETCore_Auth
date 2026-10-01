using Microsoft.AspNetCore.Mvc;

namespace EcommerceDemo.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			Console.WriteLine("Hello from home");
			return View();
		}
	}
}
