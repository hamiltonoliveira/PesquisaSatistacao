using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using System.Threading.Tasks;

namespace Infra.Mapping
{
    public class MappingConfig : Profile
    {
        public class DomainDTOMapping : Profile
        {
            public DomainDTOMapping()
            {
                CreateMap<Usuario, UsuarioDTO>();
                CreateMap<UsuarioDTO, Usuario>();

                CreateMap<Enquete, EnqueteDTO>();
                CreateMap<EnqueteDTO, Enquete>();
            }
        }
    }
}
