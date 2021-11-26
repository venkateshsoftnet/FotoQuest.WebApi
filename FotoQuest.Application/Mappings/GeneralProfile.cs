using AutoMapper;
using FotoQuest.Application.Features.Images.Commands.SaveImage;
using FotoQuest.Domain.Entities;

namespace FotoQuest.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<SaveImageCommand, Image>();
        }
    }
}
