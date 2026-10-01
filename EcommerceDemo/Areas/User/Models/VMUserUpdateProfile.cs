using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Areas.User.Models
{
	public class VMUserUpdateProfile
	{
		public int UserId { get; set; }

		[Required(ErrorMessage = "User name is required")]
		[Display(Name = "User Name")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		[Display(Name = "Email Address")]
		public string UserEmail { get; set; }


		[Required(ErrorMessage = "Gender is required")]
		public string Gender { get; set; }

		[Required(ErrorMessage = "Date of birth is required")]
		[DataType(DataType.Date)]
		[Display(Name = "Date of Birth")]
		public DateOnly DateOfBirth { get; set; }

		[Required(ErrorMessage = "Phone number is required")]
		[Phone(ErrorMessage = "Invalid phone number")]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		public string ProfileImage { get; set; }

		[Required(ErrorMessage = "Address is required")]
		public string Address { get; set; }

		[Required(ErrorMessage = "City is required")]
		public string City { get; set; }

		[Required(ErrorMessage = "State is required")]
		public string State { get; set; }

		[Required(ErrorMessage = "Postal code is required")]
		[Display(Name = "Postal Code")]
		public string PostalCode { get; set; }

		public int UpdatedByUserId { get; set; }
		public DateTime UpdatedAt { get; set; }

	}
}
