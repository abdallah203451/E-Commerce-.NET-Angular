using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Back_End.Data.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasData(
				new Category
				{
					Id = 1,
					Name = "All",
				},
				new Category
				{
					Id = 2,
					Name = "Electronics",
				},
				new Category
				{
					Id = 3,
					Name = "Compuetrs and Laptops",
				},
				new Category
				{
					Id = 4,
					Name = "Mobiles",
				},
				new Category
				{
					Id = 5,
					Name = "Fashion",
				},
				new Category
				{
					Id = 6,
					Name = "Furniture",
				},
				new Category
				{
					Id = 7,
					Name = "Books",
				},
				new Category
				{
					Id = 8,
					Name = "Sports",
				},
				new Category
				{
					Id = 9,
					Name = "Food",
				}
			);
		}
	}
}
