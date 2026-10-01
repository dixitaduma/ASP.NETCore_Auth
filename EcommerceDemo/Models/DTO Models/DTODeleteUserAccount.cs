namespace EcommerceDemo.Models.DTO_Models
{
	public class DTODeleteUserAccount
	{
		public int UserId { get; set; }
		public int? DeletedByUserId { get; set; }
		public DateTime? DeletedAt { get; set; }
	}
}
