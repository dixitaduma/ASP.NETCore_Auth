using EcommerceDemo.Models.Entity_Models;

namespace EcommerceDemo.Models.DTO_Models
{
	public class DTOProductResponse
	{

		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public decimal Price { get; set; }
		public int StockQuantity { get; set; }
		public string CategoryName { get; set; }
		public List<ProductImage> ProductImages { get; set; }
	}
}
