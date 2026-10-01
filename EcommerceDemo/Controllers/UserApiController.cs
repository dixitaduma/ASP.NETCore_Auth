using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Models;
using EcommerceDemo.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace EcommerceDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserApiController : ControllerBase
	{
		private readonly IUserInterface _userRepo;
		public UserApiController(IUserInterface userRepo)
		{
			_userRepo = userRepo;
		}
		[HttpGet("GetAllUsers")]
		public async Task<IActionResult> GetAllUsers()
		{
			ResponseModel<List<DTOUserResponse>> response = new ResponseModel<List<DTOUserResponse>>();
			try
			{
				var result = await _userRepo.GetAllUsers();
				return Ok(result);
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpGet("GetAllActiveUsers")]
		public async Task<IActionResult> GetAllActiveUsers()
		{
			ResponseModel<List<DTOUserResponse>> response = new ResponseModel<List<DTOUserResponse>>();
			try
			{
				var result = await _userRepo.GetAllActiveUsers();
				return Ok(result);
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpGet("GetAllDeactiveUsers")]
		public async Task<IActionResult> GetAllDeactiveUsers()
		{
			ResponseModel<List<DTOUserResponse>> response = new ResponseModel<List<DTOUserResponse>>();
			try
			{
				var result = await _userRepo.GetAllDeactiveUsers();
				return Ok(result);
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpGet("GetOneUser")]
		public async Task<IActionResult> GetOneUser(int id)
		{
			ResponseModel<List<DTOUserResponse>> response = new ResponseModel<List<DTOUserResponse>>();
			try
			{
				var result = await _userRepo.GetOneUser(id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPut("DeleteUser")]
		public async Task<IActionResult> DeleteUser(DTODeleteUserAccount DeleteUser)
		{
			ResponseModel<object> response = new ResponseModel<object>();
			try
			{
				var result = await _userRepo.DeleteUserAccount(DeleteUser);
				return Ok(result);
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPut("UpdateUserDetails")]
		public async Task<IActionResult> UpdateUserDetails([FromBody] DTOUpdateUserProfile user)
		{
			ResponseModel<object> response = new ResponseModel<object>();
			try
			{
				var result = await _userRepo.UpdateUserDetails(user);
				return Ok(result);
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = ex.Message;
				return BadRequest(response);
			}
		}
	}
}
