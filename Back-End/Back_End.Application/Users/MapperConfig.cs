using AutoMapper;
using Back_End.Domain.Entities;

namespace Back_End.Application.Users
{
	public class MapperConfig : Profile
	{
		public MapperConfig() 
		{ 
			CreateMap<UserDto,User>().ReverseMap();

			CreateMap<ProfileDataDto, User>().ReverseMap();
		}
	}
}
