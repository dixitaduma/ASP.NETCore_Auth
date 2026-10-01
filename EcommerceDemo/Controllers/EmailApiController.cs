using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace EcommerceDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmailApiController : ControllerBase
	{
		private readonly IEmailServiceInterface _emailservice;
		public EmailApiController(IEmailServiceInterface emailservice)
		{
			_emailservice = emailservice;
		}

		[HttpPost("SendOtp")]
		public async Task<IActionResult> SendOtp(DTOSendEmailOtp SendEmailOtp)
		{
			try
			{
				var result = await _emailservice.SendOtpEmailAsync(SendEmailOtp);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}
