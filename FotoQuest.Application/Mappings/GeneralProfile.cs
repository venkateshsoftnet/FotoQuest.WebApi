using AutoMapper;

using FotoQuest.Application.Features.Images.Commands.SaveImage;
using FotoQuest.Domain.Entities;

using Microsoft.AspNetCore.Http;

namespace FotoQuest.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<IFormFile, Image>();

            //CreateMap<SaveImageCommand, Image>();
        }
    }
}
