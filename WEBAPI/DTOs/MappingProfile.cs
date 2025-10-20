using AutoMapper;
using WEBAPI.Models;

namespace WEBAPI.DTOs
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<RolDto, Role>();
		}
	}
}
