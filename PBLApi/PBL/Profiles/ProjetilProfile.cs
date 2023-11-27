using AutoMapper;
using PBL.Data.Dtos;
using PBL.Models;

namespace PBL.Profiles
{
    public class ProjetilProfile : Profile
    {
        public ProjetilProfile()
        {
            CreateMap<CreateProjetilDto, ProjetilModel>();
            CreateMap<ProjetilModel, ReadProjetilDto>();            
        }
    }
}
