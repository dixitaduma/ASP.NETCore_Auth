using System.Net.Mail;
using System.Net;
using EcommerceDemo.Models;
using EcommerceDemo.Repository.Interface;
using Microsoft.Extensions.Options;
using EcommerceDemo.Models.DTO_Models;
namespace EcommerceDemo.Repository.Implementation
{
	public class EmailServiceRepository : IEmailServiceInterface
	{
		private readonly DTOEmailSettings _emailSettings;
		public EmailServiceRepository(IOptions<DTOEmailSettings> emailSettings)
		{
			_emailSettings = emailSettings.Value;
		}
		public ResponseModel<string> response = new ResponseModel<string>();
		public async Task<ResponseModel<string>> SendOtpEmailAsync(DTOSendEmailOtp SendEmailOtp)
		{
			try
			{
				using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
				{
					Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
					EnableSsl = _emailSettings.EnableSsl
				};
				var mailMessage = new MailMessage
				{
					From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
					Subject = SendEmailOtp.Subject,
					Body = SendEmailOtp.Body,
					IsBodyHtml = true
				};
				mailMessage.To.Add(SendEmailOtp.ToEmail);
				await client.SendMailAsync(mailMessage);
				response.statusCode = (int)HttpStatusCode.OK;
				response.messages = "Email sent successfully.";
				response.data = "Success";
			}
			catch (SmtpException smtpEx)
			{
				response.statusCode = (int)HttpStatusCode.BadRequest;
				response.messages = $"SMTP Error: {smtpEx.Message}";
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"Error sending email: {ex.Message}";
			}
			return response;
		}
	}
}
