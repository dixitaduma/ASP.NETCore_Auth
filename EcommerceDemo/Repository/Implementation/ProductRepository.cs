using Azure;
using System.Net;
using System.Security.Cryptography;
using EcommerceDemo.ApplicationDbContext;
using EcommerceDemo._commanmethods;
using EcommerceDemo.Models;
using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Models.Entity_Models;
using EcommerceDemo.Repository.Interface;
using Microsoft.EntityFrameworkCore;
namespace EcommerceDemo.Repository.Implementation
{
	public class ProductRepository : IProductInterface
	{
		private readonly AppDbContext _appDbContext;
		private readonly CommanMethods _commanMethods;
		public ProductRepository(AppDbContext appDbContext, CommanMethods commanMethods, IEmailServiceInterface emailService)
		{
			_appDbContext = appDbContext;
			_commanMethods = commanMethods;
		}
		public ResponseModel<object> response = new ResponseModel<object>();
		public async Task<ResponseModel<object>> AddProduct(DTOAddProduct Productdata)
		{
			using var transaction = await _appDbContext.Database.BeginTransactionAsync();
			try
			{
				//Check is Product with same name and category is already exist
				var IsProductAreadyExists = await _appDbContext.Products
					.FirstOrDefaultAsync(p => p.ProductName == Productdata.ProductName && p.CategoryId == Productdata.CategoryId);
				if (IsProductAreadyExists != null)
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.productAlreadyExists;
					response.data = null;
					return response;
				}
				//Bind Data to Product Model
				var product = new Product
				{
					ProductName = Productdata.ProductName,
					ProductDescription = Productdata.ProductDescription,
					Price = Productdata.Price,
					StockQuantity = Productdata.StockQuantity,
					CategoryId = Productdata.CategoryId
				};
				//Store Productdata to database
				var addProductResult = await _appDbContext.Products.AddAsync(product);
				await _appDbContext.SaveChangesAsync();
				foreach (var images in Productdata.ProductImages)
				{
					var ImagesData = new ProductImage
					{
						ProductId = product.ProductId,
						ProductImageName = images.ProductImageName,
						ProductImageType = images.ProductImageType
					};
					await _appDbContext.ProductImages.AddAsync(ImagesData);
				}
				var saveResult = await _appDbContext.SaveChangesAsync();
				if (saveResult > 0)
				{
					await transaction.CommitAsync();
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.addProductSuccess;
				}
				else
				{
					await transaction.RollbackAsync();
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.addProductFailed;
				}
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
		public async Task<ResponseModel<object>> UpdateProduct(DTOUpdateProduct Productdata)
		{
			using var transaction = await _appDbContext.Database.BeginTransactionAsync();
			try
			{
				var product = await _appDbContext.Products
					.Include(p => p.ProductImages)
					.FirstOrDefaultAsync(p => p.ProductId == Productdata.ProductId);
				if (product == null)
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.productNotFound;
					await transaction.RollbackAsync();
					return response;
				}
				product.ProductName = Productdata.ProductName;
				product.ProductDescription = Productdata.ProductDescription;
				product.Price = Productdata.Price;
				product.StockQuantity = Productdata.StockQuantity;
				product.CategoryId = Productdata.CategoryId;
				var incomingimages = Productdata.ProductImages ?? new List<DTOUpdateProductImage>();
				foreach (var image in incomingimages)
				{
					var existingimage = await _appDbContext.ProductImages.FirstOrDefaultAsync(i => image.ProductImageId == image.ProductImageId);
					if (existingimage != null)
					{
						existingimage.ProductImageName = image.ProductImageName;
						existingimage.ProductImageType = image.ProductImageType;
						existingimage.UpdatedAt = DateTime.UtcNow;
						existingimage.UpdatedByUserId = Productdata.UpdatedByUserId;
					}
				}
				var saveResult = await _appDbContext.SaveChangesAsync();
				if (saveResult > 0)
				{
					await transaction.CommitAsync();
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.updateProductSuccess;
				}
				else
				{
					await transaction.RollbackAsync();
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.updateProductFailed;
				}
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
		public async Task<ResponseModel<object>> DeleteProduct(DTODeleteProduct Productdata)
		{
			using var transaction = await _appDbContext.Database.BeginTransactionAsync();
			try
			{
				//Check is Product with same name and category is already exist
				var product = await _appDbContext.Products.Include(p => p.ProductImages)
					.FirstOrDefaultAsync(p => p.ProductId == Productdata.ProductId);
				if (product == null)
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.productNotFound;
					response.data = null;
					return response;
				}
				product.DeletedAt = DateTime.Now;
				product.IsDeleted = true;
				product.DeletedByUserId = Productdata.DeletedByUserId;
				var ProductImageList = await _appDbContext.ProductImages.Where(p => p.ProductId == product.ProductId).ToListAsync();
				foreach (var image in ProductImageList)
				{
					image.IsDeleted = true;
					image.DeletedByUserId = Productdata.DeletedByUserId;
					image.DeletedAt = DateTime.UtcNow;
				}
				var saveResult = await _appDbContext.SaveChangesAsync();
				if (saveResult > 0)
				{
					await transaction.CommitAsync();
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.deleteProductSuccess;
				}
				else
				{
					await transaction.RollbackAsync();
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.deleteProductFailed;
				}
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
	}
}
