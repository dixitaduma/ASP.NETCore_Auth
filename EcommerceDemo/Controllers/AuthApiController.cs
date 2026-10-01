using System.Net;
using EcommerceDemo._commanmethods;
using EcommerceDemo.Models;
using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Repository.Interface;
using EcommerceDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
namespace EcommerceDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthApiController : ControllerBase
	{
		private readonly IAuthInterface _authrepo;
		private readonly CommanMethods _commanMethods;
		private readonly AuthService _authService;
		public ResponseModel<object> response = new ResponseModel<object>();
		//private readonly   _authrepo;
		public AuthApiController(IAuthInterface authrepo, CommanMethods commanMethods, AuthService authService)
		{
			_authrepo = authrepo;
			_commanMethods = commanMethods;
			_authService = authService;
		}
		[HttpPost("UserRegistration")]
		public async Task<IActionResult> UserRegistration(DTOUserRegistration UserData)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.modelStateInvalid;
					response.data = ModelState;
					return BadRequest(response);
				}
				else
				{
					var result = await _authrepo.UserRegistration(UserData);
					return Ok(result);
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login(DTOUserLogin UserData)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.modelStateInvalid;
					return BadRequest(response);
				}
				else
				{
					var result = await _authService.Login(UserData);
					return Ok(result);
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("GenerateOtp")]
		public async Task<IActionResult> GenerateOtp(DTOGenerateOtp userdata)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.modelStateInvalid;
					response.data = ModelState;
					return BadRequest(response);
				}
				else
				{
					var result = await _authrepo.GenerateOtp(userdata);
					return Ok(result);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpPost("OtpVerification")]
		public async Task<IActionResult> OtpVerification(DTOOtpVerificationModel OtpData)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.modelStateInvalid;
					response.data = ModelState;
					return BadRequest(response);
				}
				else
				{
					var result = await _authrepo.OtpVrification(OtpData);
					return Ok(result);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpPost("ForgetPassword")]
		public async Task<IActionResult> ForgetPassword(DTOForgatePassword UserCredential)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.modelStateInvalid;
					response.data = ModelState;
					return BadRequest(response);
				}
				else
				{
					var result = await _authrepo.ForgetPassword(UserCredential);
					return Ok(result);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}
