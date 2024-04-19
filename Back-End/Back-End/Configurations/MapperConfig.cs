using AutoMapper;
using Back_End.Data;
using Back_End.Models.Users;

namespace Back_End.Configurations
{
	public class MapperConfig : Profile
	{
		public MapperConfig() 
		{ 
			CreateMap<UserDto,User>().ReverseMap();
		}
	}
}
