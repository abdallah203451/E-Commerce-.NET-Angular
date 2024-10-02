using Back_End.Application.Products;
using Back_End.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Services
{
	public interface ITrendingProductsCacheService
	{
		Task<PagedResult<ProductDto>> GetTrendingProductsAsync(ProductFilterDto filterDto, int pageSize);

		void ClearTrendingProductsCache();
	}
}
