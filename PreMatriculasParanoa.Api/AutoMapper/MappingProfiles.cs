using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using PreMatriculasParanoa.Domain.Models.ViewModels;
using PreMatriculasParanoa.Domain.Models.Entities;

namespace Cened.Penitenciario.Api.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
        }
    }
}
