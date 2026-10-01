using EcommerceDemo._commanmethods;
using EcommerceDemo.Areas.User.Models;
using Microsoft.AspNetCore.Http;
using EcommerceDemo.Areas.User.Services.IRepository;
using Microsoft.AspNetCore.Mvc;
using EcommerceDemo.Models;
using System.Net;
using Azure;
namespace EcommerceDemo.Areas.User.Controllers
{
	[Area("User")]
	public class ManageUserController : Controller
	{
		private readonly IManageUserInterface _manageUser;
		private readonly CommanMethods _commanMethods;
		public ManageUserController(IManageUserInterface manageUser, CommanMethods commanMethods)
		{
			_manageUser = manageUser;
			_commanMethods = commanMethods;
		}
		public IActionResult Index()
		{
			return View();
		}
		//To get profile page
		public async Task<IActionResult> UserProfile()
		{
			try
			{
				int id = 0;
				if (HttpContext.Session.GetInt32("UserId") >= 1 && HttpContext.Session.GetString("UserEmail") != null)
				{
					id = (int)HttpContext.Session.GetInt32("UserId");
				}
				else
				{
					return RedirectToAction("Login", "UserAuth", new { area = "User" });
				}
				var responce = await _manageUser.GetUserProfile(id);
				if (responce.statusCode == 200)
				{
					var responceData = responce.data;
					if (responceData != null)
					{
						var userdata = new VMUserProfile()
						{
							UserId = responceData.UserId,
							UserEmail = responceData.UserEmail,
							UserName = responceData.UserName,
							Gender = responceData.Gender,
							DateOfBirth = responceData.DateOfBirth,
							PhoneNumber = responceData.PhoneNumber,
							ProfileImage = responceData.ProfileImage,
							Address = responceData.Address,
							City = responceData.City,
							State = responceData.State,
							PostalCode = responceData.PostalCode,
						};
						return View(userdata);
					}
					else
					{
						ModelState.AddModelError("", responce.messages);
					}
				}
				else
				{
					ModelState.AddModelError("", responce.messages);
				}
				return View(responce);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Unexpected error " + ex.Message);
				return View();
			}
		}
		[HttpPost]
		public async Task<IActionResult> UserProfile(VMUserProfile UserData)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					//ViewBag.ModelsStateInvalidError = ModelState;
					return View(UserData);
				}
				int id = 0;
				if (HttpContext.Session.GetInt32("UserId") >= 1 && HttpContext.Session.GetString("UserEmail") != null)
				{
					id = (int)HttpContext.Session.GetInt32("UserId");
				}
				UserData.UpdatedAt = DateTime.Now;
				UserData.UpdatedByUserId = id;
				var responce = await _manageUser.UpdateUserProfile(UserData);
				if (responce.statusCode == 200)
				{
					TempData["successMessage"] = responce.messages;
					ViewBag.successMessage = responce.messages;
					//return RedirectToAction("Home", "UserHome", new { area = "User" });
					return View(UserData);
				}
				else
				{
					ModelState.AddModelError("", responce.messages);
				}
				return View(UserData);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Unexpected error " + ex.Message);
				return View(UserData);
			}
		}
		//User Account deleted by user it self
		[HttpGet]
		public async Task<IActionResult> DeleteUserAccount()
		{
			var responseModel = new ResponseModel<object>();
			try
			{
				int id = 0;
				if (HttpContext.Session.GetInt32("UserId") >= 1 && HttpContext.Session.GetString("UserEmail") != null)
				{
					id = (int)HttpContext.Session.GetInt32("UserId");
				}
				else
				{
					responseModel.statusCode = (int)HttpStatusCode.BadRequest;
					responseModel.messages = _commanMethods.deleteUserfailed;
					return BadRequest(responseModel);
				}
				var deleteUserData = new VMDeleteUserAccount()
				{
					UserId = id,
					DeletedByUserId = id
				};
				var responce = await _manageUser.DeleteUserProfile(deleteUserData);
				if (responce.statusCode == 200)
				{
					HttpContext.Session.Clear();
					return Ok(responce);
				}
				else
				{
					return BadRequest(responce);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", "Unexpected error " + ex.Message);
				return BadRequest(responseModel);
			}
		}
	}
}
