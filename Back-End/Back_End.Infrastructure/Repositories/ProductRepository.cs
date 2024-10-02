using Back_End.Domain.Entities;
using Back_End.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Back_End.Application.Repositories;
using Back_End.Application.Products;
using System.Drawing.Printing;
using Microsoft.CodeAnalysis;

namespace Back_End.Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _context;
		public ProductRepository(ApplicationDbContext context) 
		{
			_context = context;
		}
		public async Task<bool> AddProduct(Product product) 
		{
			if(product == null)
			{
				return false;
			}
			product.Id = 0;
			product.AddDate = DateTime.UtcNow;
			await _context.AddAsync(product);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<PagedResult<Product>> GetAllProducts(ProductFilterDto filterDto, int pageSize)
		{
			// Start with the base query
			IQueryable<Product> productsQuery = _context.Products;

			// Apply category filter (1 means all categories, skip filtering if it's 1)
			if (filterDto.Category != 1)
			{
				productsQuery = productsQuery.Where(p => p.CategoryId == filterDto.Category);
			}

			// Apply price filter (check MinPrice and MaxPrice are provided)
			if (filterDto.MinPrice > 0)
			{
				productsQuery = productsQuery.Where(p => p.Price >= filterDto.MinPrice);
			}
			if (filterDto.MaxPrice > 0)
			{
				productsQuery = productsQuery.Where(p => p.Price <= filterDto.MaxPrice);
			}

			// Apply name filter (if Name is provided)
			if (!string.IsNullOrEmpty(filterDto.Name))
			{
				productsQuery = productsQuery.Where(p => p.Name.Contains(filterDto.Name));
			}

			// Pagination logic
			var totalItems = await productsQuery.CountAsync(); // Get total count after filtering
			var products = await productsQuery
				.Skip((filterDto.PageNumber - 1) * pageSize) // Skip for pagination
				.Take(pageSize)                             // Take the page size
				.ToListAsync();

			// Return paginated result
			return new PagedResult<Product>
			{
				Items = products,
				TotalItems = totalItems,
				PageNumber = filterDto.PageNumber,
				PageSize = pageSize
			};
		}

		public async Task<PagedResult<ProductDto>> GetTrendingProducts(ProductFilterDto filterDto, int pageSize)
		{
			var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);

			// Start building the query, applying filters first
			var trendingProductsQuery = _context.OrderItems
				.Where(oi => oi.Order.Date >= sevenDaysAgo) // Filter by last 7 days
				.GroupBy(oi => new { oi.Product.Id, oi.Product.Name, oi.Product.Price, oi.Product.Image, oi.Product.CategoryId })
				.Select(g => new
				{
					ProductId = g.Key.Id,
					Name = g.Key.Name,
					Price = g.Key.Price,
					Image = g.Key.Image,
					CategoryId = g.Key.CategoryId,
					TotalQuantitySold = g.Sum(oi => oi.quantity) // Calculate total quantity sold
				});

			// Apply category filter (1 means all categories)
			if (filterDto.Category != 1)
			{
				trendingProductsQuery = trendingProductsQuery.Where(p => p.CategoryId == filterDto.Category);
			}

			// Apply price filter
			if (filterDto.MinPrice > 0)
			{
				trendingProductsQuery = trendingProductsQuery.Where(p => p.Price >= filterDto.MinPrice);
			}
			if (filterDto.MaxPrice > 0)
			{
				trendingProductsQuery = trendingProductsQuery.Where(p => p.Price <= filterDto.MaxPrice);
			}

			// Apply name filter
			if (!string.IsNullOrEmpty(filterDto.Name))
			{
				trendingProductsQuery = trendingProductsQuery.Where(p => p.Name.Contains(filterDto.Name));
			}

			// After all filters are applied, sort by total quantity sold
			trendingProductsQuery = trendingProductsQuery.OrderByDescending(p => p.TotalQuantitySold);

			// Pagination logic
			var totalItems = await trendingProductsQuery.CountAsync();
			var trendingProducts = await trendingProductsQuery
				.Skip((filterDto.PageNumber - 1) * pageSize) // Skip for pagination
				.Take(pageSize)                             // Take the page size
				.ToListAsync();

			// Convert to DTOs
			var productDtos = trendingProducts.Select(p => new ProductDto
			{
				Id = p.ProductId,
				Name = p.Name,
				Price = p.Price,
				Image = p.Image,
			}).ToList();

			// Return paginated result
			return new PagedResult<ProductDto>
			{
				Items = productDtos,
				TotalItems = totalItems,
				PageNumber = filterDto.PageNumber,
				PageSize = pageSize
			};
		}

		public async Task<Product> GetProductDetails(int Id)
		{
			Product product =  await _context.Products.FirstOrDefaultAsync(p => p.Id == Id);
			return product;
		}

		public async Task<List<UserProductDto>> GetUserProductsAsync(string userId)
		{
			// Fetch products added by the user
			var products = await _context.Products
				.Include(p => p.OrderItems) // Include related OrderItems
				.Where(p => p.UserId == userId)
				.ToListAsync();

			// Map products to UserProductDto
			var userProducts = products.Select(p => new UserProductDto
			{
				Product = p,
				// QuantitySold is calculated as the sum of ordered quantities (OrderItems)
				QuantitySold = p.OrderItems.Sum(oi => oi.quantity)
			}).ToList();

			return userProducts;
		}

		public async Task<bool> DeleteProductAsync(int id)
		{
			// Fetch products added by the user
			Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

			if(product == null) 
			{
				return false;
			}

			_context.Products.Remove(product);

			_context.SaveChanges();

			return true;
		}

		public async Task<bool> EditProduct(Product product)
		{
			if (product == null)
			{
				return false;
			}
			product.AddDate = DateTime.UtcNow;
			_context.Update(product);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
