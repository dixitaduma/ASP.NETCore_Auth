namespace EcommerceDemo.Models.DTO_Models
{
	public class DTOUserResponse
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string UserEmail { get; set; }

		public string Password { get; set; }
		public string Gender { get; set; }
		public DateOnly DateOfBirth { get; set; }
		public string PhoneNumber { get; set; }
		public string ProfileImage { get; set; }

		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string RoleName { get; set; }

		public bool IsOtpVerified { get; set; } = false;
		public bool IsDeleted { get; set; }




	}
}
