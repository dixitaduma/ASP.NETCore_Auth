namespace EcommerceDemo.Models.DTO_Models
{
	public class DTOUpdateProduct
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public decimal Price { get; set; }
		public int StockQuantity { get; set; }
		public List<DTOUpdateProductImage> ProductImages { get; set; }

		public int CategoryId { get; set; }
		public int UpdatedByUserId { get; set; }

	}
}
