using AutoMapper;
using Back_End.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back_End.Application.Carts
{
	public class CartMapper : Profile
	{
		public CartMapper()
		{
			CreateMap<CartItem, CartItemDto>()
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
				.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Product.Image))
				.ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Product.Discount));
		}
	}
}
