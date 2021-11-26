using FotoQuest.WebApi.Application.Exceptions;
using FotoQuest.WebApi.Application.Interfaces.Repositories;
using FotoQuest.WebApi.Application.Interfaces.Services;
using FotoQuest.WebApi.Application.Wrappers;
using FotoQuest.WebApi.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FotoQuest.WebApi.Application.Features.Images.Queries.GetImageById
{
    public class GetImageByIdQuery : IRequest<Response<Image>>
    {
        public Guid Id { get; set; }

        public ImageType ImageType { get; set; }

        public int CustomSize { get; set; }

        public class GetImageByIdQueryHandler : IRequestHandler<GetImageByIdQuery, Response<Image>>
        {
            private readonly IImageRepositoryAsync _imageRepository;

            private readonly IImageService _imageService;

            public GetImageByIdQueryHandler(IImageRepositoryAsync imageRepository, IImageService imageService)
            {
                _imageRepository = imageRepository;
                _imageService = imageService;
            }

            public async Task<Response<Image>> Handle(GetImageByIdQuery query, CancellationToken cancellationToken)
            {
                var image = await _imageRepository.GetByIdAsync(query.Id);
                if (image == null) throw new ApiException($"Image Not Found.");

                var response = await _imageService.GetFile(query.Id, image.FileName, query.ImageType, query.CustomSize);

                image.FileContent = Convert.ToBase64String(response.ToArray());

                return new Response<Image>(image);
            }
        }
    }
}
