using AutoMapper;
using PBL.Data.Dtos;
using PBL.Models;

namespace PBL.Profiles
{
    public class ColisaoProfile : Profile
    {
        public ColisaoProfile()
        {
            CreateMap<CreateColisaoDto, ColisaoModel>();
            CreateMap<ColisaoModel, ReadColisaoDto>();
            CreateMap<ReadColisaoDto, ColisaoModel>();
        }
    }
}
