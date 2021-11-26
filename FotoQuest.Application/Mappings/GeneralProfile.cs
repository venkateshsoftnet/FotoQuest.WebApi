using AutoMapper;
using FotoQuest.WebApi.Application.Features.Images.Commands.SaveImage;
using FotoQuest.WebApi.Application.Features.Products.Commands.CreateProduct;
using FotoQuest.WebApi.Application.Features.Products.Queries.GetAllProducts;
using FotoQuest.WebApi.Domain.Entities;

namespace FotoQuest.WebApi.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();

            CreateMap<CreateProductCommand, Product>();

            CreateMap<SaveImageCommand, Image>();

            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
