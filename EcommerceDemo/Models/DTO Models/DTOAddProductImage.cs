using EcommerceDemo.Models.Entity_Models;
namespace EcommerceDemo.Models.DTO_Models
{
	public class DTOAddProductImage
	{
		public string ProductImageName { get; set; }
		public ImageType ProductImageType { get; set; }
		public int? ProductId { get; set; }
	}
}
