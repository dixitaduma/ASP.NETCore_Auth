namespace EcommerceDemo.Areas.User.Models
{
	public class VMOtpModel
	{
		public string Otp { get; set; }
		public string UserEmail { get; set; }
		public bool IsForgetPassword { get; set; } = false;
	}
}
