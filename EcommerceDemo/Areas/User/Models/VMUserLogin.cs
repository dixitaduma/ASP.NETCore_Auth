using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Areas.User.Models
{
	public class VMUserLogin
	{
		[Required(ErrorMessage = " Username or Email is required")]
		[Display(Name = "Username/Email ")]
		public string UserEmail { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

	}
}
