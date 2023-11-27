using AutoMapper;
using PBL.Data.Dtos;
using PBL.Models;

namespace PBL.Profiles
{
    public class MeteoroProfile : Profile
    {
        public MeteoroProfile()
        {
            CreateMap<CreateMeteoroDto, MeteoroModel>();
            CreateMap<MeteoroModel, ReadMeteoroDto>();
        }
    }
}
