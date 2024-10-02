using Back_End.Application.Products;
using Back_End.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Repositories
{
	public interface IProductRepository
	{
		Task<bool> AddProduct(Product product);

		Task<PagedResult<Product>> GetAllProducts(ProductFilterDto filterDto, int pageSize);

		Task<PagedResult<ProductDto>> GetTrendingProducts(ProductFilterDto filterDto, int pageSize);

		Task<Product> GetProductDetails(int Id);

		Task<List<UserProductDto>> GetUserProductsAsync(string userId);

		Task<bool> DeleteProductAsync(int id);

		Task<bool> EditProduct(Product product);

	}
}
