namespace EcommerceDemo.Models.DTO_Models
{
	public class DTOUserRegistration
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string UserEmail { get; set; }
		public string Password { get; set; }
		public string Gender { get; set; }
		public DateOnly DateOfBirth { get; set; }
		public string PhoneNumber { get; set; }
		public string? ProfileImage { get; set; }

		//public IFormFile? ProfileImageFile { get; set; }

		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }


		//public byte[] Key { get; set; }

		public int RoleId { get; set; }



	}
}