using Azure;
using EcommerceDemo.Models;
using EcommerceDemo.Models.DTO_Models;

namespace EcommerceDemo.Repository.Interface
{
	public interface IEmailServiceInterface
	{
		Task<ResponseModel<string>> SendOtpEmailAsync(DTOSendEmailOtp SendEmailOtp);
	}
}
