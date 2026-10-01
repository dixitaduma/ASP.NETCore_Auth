namespace EcommerceDemo.Models.DTO_Models
{
	public class DTOForgatePassword
	{
		public string UserEmail { get; set; }
		public string Password { get; set; }

		public bool IsOtpVerified { get; set; } = false;
	}
}
