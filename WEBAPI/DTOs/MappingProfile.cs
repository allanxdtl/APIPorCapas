using AutoMapper;
using WEBAPI.Models;

namespace WEBAPI.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RolDto, Role>();
            CreateMap<ClienteDto, Cliente>();
            CreateMap<ProductoDto, Producto>();

            // Detalles de venta
            CreateMap<RenglonesDto, VentaDetalle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IdVentaHeader, opt => opt.Ignore()) // Se asigna en el repositorio
                .ForMember(dest => dest.VentaHeader, opt => opt.Ignore());   // Ignorar navegación

            // Venta Header
            CreateMap<VentaDto, VentaHeader>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Detalles, opt => opt.Ignore()) // Lo hacemos manual con AfterMap
                .AfterMap((src, dest, ctx) =>
                {
                    dest.Detalles = src.Renglones
                        .Select(r => ctx.Mapper.Map<VentaDetalle>(r))
                        .ToList();
                });
        }
    }
}
