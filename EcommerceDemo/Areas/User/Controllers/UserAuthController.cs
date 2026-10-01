using EcommerceDemo.Areas.User.Models;
using EcommerceDemo.Areas.User.Services.IRepository;
using EcommerceDemo._commanmethods;
using EcommerceDemo.Models.Entity_Models;
using Microsoft.AspNetCore.Mvc;
namespace EcommerceDemo.Areas.User.Controllers
{
	[Area("User")]
	public class UserAuthController : Controller
	{
		private readonly IUserAuthInterface _userAuthRepo;
		private readonly CommanMethods _commanMethods;
		public UserAuthController(IUserAuthInterface userAuthRepo, CommanMethods commanMethods)
		{
			_userAuthRepo = userAuthRepo;
			_commanMethods = commanMethods;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult UserRegistration()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> UserRegistration(VMRegisterUser IncomingUserData)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(IncomingUserData);
				}
				var responce = await _userAuthRepo.UserRegistration(IncomingUserData);
				if (responce.statusCode == 200)
				{
					HttpContext.Session.SetString("UserEmail", IncomingUserData.UserEmail);
					return RedirectToAction("OtpVerification");
				}
				else
				{
					ModelState.AddModelError("", responce.messages);
				}
				return View(IncomingUserData);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Unexpected error " + ex.Message);
				return View(IncomingUserData);
			}
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(VMUserLogin user)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(user);
				}
				var responce = await _userAuthRepo.Login(user);
				if (responce.statusCode == 200)
				{
					if (responce.data != null)
					{
						var data = responce.data;
						HttpContext.Session.SetString("UserEmail", data.UserEmail);
						HttpContext.Session.SetInt32("UserId", data.UserId);
						HttpContext.Session.SetString("UserName", data.UserName);
					}
					if (responce.data.RoleName == "Admin")
					{
						return RedirectToAction("Home", "AdminHome", new { area = "Admin" });
					}
					else if (responce.data.RoleName == "User")
					{
						return RedirectToAction("Home", "UserHome", new { area = "User" });
					}
					else
					{
						return View(user);
					}
				}
				else
				{
					ModelState.AddModelError("", responce.messages);
				}
				return View(user);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Unexpected error " + ex.Message);
				return View(user);
			}
		}
		public IActionResult GenerateOtp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> GenerateOtp(VMGenerateOtp userdata)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					//ViewBag.ModelsStateInvalidError = ModelState;
					return View(userdata);
				}
				var responce = await _userAuthRepo.GenerateOtp(userdata);
				if (responce.statusCode == 200)
				{
					HttpContext.Session.SetString("UserEmail", userdata.UserEmail);
					//HttpContext.Session.SetString("isForgetPassword", "true");
					TempData["isForgetPassword"] = true;
					return RedirectToAction("OtpVerification", "UserAuth", new { area = "User" });
				}
				else
				{
					ModelState.AddModelError("", responce.messages);
				}
				return View(userdata);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Unexpected error " + ex.Message);
				return View(userdata);
			}
		}
		public IActionResult OtpVerification()
		{
			if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
			{
				bool? isForgetPassword = (bool?)TempData["isForgetPassword"];
				//string isForgetPassword = HttpContext.Session.GetString("isForgetPassword")
				string email = HttpContext.Session.GetString("UserEmail");
				var otpData = new VMOtpModel()
				{
					UserEmail = email,
					IsForgetPassword = isForgetPassword ?? false,
				};
				return View(otpData);
			}
			else
			{
				ViewBag.Erorr = _commanMethods.nullItem("UserEmail");
				return View();
			}
		}
		[HttpPost]
		public async Task<IActionResult> OtpVerification(VMOtpModel otpdata)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					//ViewBag.ModelsStateInvalidError = ModelState;
					return View(otpdata);
				}
				var responce = await _userAuthRepo.OtpVerification(otpdata);
				if (responce.statusCode == 200)
				{
					HttpContext.Session.SetString("isUserOtpVerified", "true");
					if (otpdata.IsForgetPassword)
					{
						return RedirectToAction("ForgetPassword", "UserAuth", new { area = "User" });
					}
					else
					{
						HttpContext.Session.Clear();
						return RedirectToAction("Login", "UserAuth", new { area = "User" });
					}
				}
				else
				{
					ModelState.AddModelError("", responce.messages);
				}
				return View(otpdata);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Unexpected error " + ex.Message);
				return View(otpdata);
			}
		}
		public IActionResult ForgetPassword()
		{
			if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
			{
				string email = HttpContext.Session.GetString("UserEmail");
				bool isUserOtpVerified = !string.IsNullOrEmpty(HttpContext.Session.GetString("isUserOtpVerified"));
				var forgetPasswordData = new VMForgetPassword()
				{
					UserEmail = email,
					IsOtpVerified = isUserOtpVerified
				};
				return View(forgetPasswordData);
			}
			else
			{
				return View();
			}
		}
		[HttpPost]
		public async Task<IActionResult> ForgetPassword(VMForgetPassword userdata)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					//ViewBag.ModelsStateInvalidError = ModelState;
					return View(userdata);
				}
				var responce = await _userAuthRepo.ForgetPassword(userdata);
				if (responce.statusCode == 200)
				{
					HttpContext.Session.Clear();
					return RedirectToAction("Login");
				}
				else
				{
					ModelState.AddModelError("", responce.messages);
				}
				return View(userdata);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Unexpected error " + ex.Message);
				return View(userdata);
			}
		}
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login", "UserAuth", new { area = "User" });
		}
	}
}
