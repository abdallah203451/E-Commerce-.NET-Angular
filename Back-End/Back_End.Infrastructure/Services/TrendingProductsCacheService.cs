using Back_End.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Back_End.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Back_End.Application.Products;
using Back_End.Application.Services;

namespace Back_End.Infrastructure.Services
{
	public class TrendingProductsCacheService : ITrendingProductsCacheService
	{
		private readonly IProductRepository _productRepository;
		private readonly IMemoryCache _cache;
		private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30); // Adjust cache duration as needed

		private const string TrendingProductsCacheKey = "TrendingProducts";

		public TrendingProductsCacheService(IProductRepository productRepository, IMemoryCache cache)
		{
			_productRepository = productRepository;
			_cache = cache;
		}

		public async Task<PagedResult<ProductDto>> GetTrendingProductsAsync(ProductFilterDto filterDto, int pageSize)
		{
			if (!_cache.TryGetValue(TrendingProductsCacheKey, out PagedResult<ProductDto> trendingProducts))
			{
				// If not found in cache, fetch from database
				trendingProducts = await _productRepository.GetTrendingProducts(filterDto, pageSize);

				// Set cache options
				var cacheEntryOptions = new MemoryCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = _cacheDuration
				};

				// Set data in cache
				_cache.Set(TrendingProductsCacheKey, trendingProducts, cacheEntryOptions);
			}

			return trendingProducts;
		}

		public void ClearTrendingProductsCache()
		{
			_cache.Remove(TrendingProductsCacheKey);
		}
	}
}
