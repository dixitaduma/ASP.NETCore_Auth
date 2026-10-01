using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Areas.User.Models
{
	public class VMForgetPassword
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		[Display(Name = "Email Address")]
		public string UserEmail { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm password is required")]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }

		public bool IsOtpVerified { get; set; } = false;
	}
}
