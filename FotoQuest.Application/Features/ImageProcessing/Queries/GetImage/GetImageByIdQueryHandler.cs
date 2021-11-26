using System.Threading;
using System.Threading.Tasks;

using FotoQuest.Application.Exceptions;
using FotoQuest.Application.Interfaces.Repositories;
using FotoQuest.Application.Interfaces.Services;
using FotoQuest.Application.Model;
using FotoQuest.Application.Wrappers;
using FotoQuest.Domain.Entities;

using MediatR;

namespace FotoQuest.Application.Features.Images.Queries.GetImageById
{
    //public class GetImageByIdQuery : IRequest<Response<Image>>
    //{
    //public Guid Id { get; set; }

    //public ImageType ImageType { get; set; }

    //public int CustomSize { get; set; }

    public class GetImageByIdQueryHandler : IRequestHandler<GetImageRequestQuery, Response<FileDataResponse>>
    {
        private readonly IImageRepositoryAsync _imageRepository;

        private readonly IImageService _imageService;

        public GetImageByIdQueryHandler(IImageRepositoryAsync imageRepository, IImageService imageService)
        {
            _imageRepository = imageRepository;
            _imageService = imageService;
        }

        public async Task<Response<FileDataResponse>> Handle(GetImageRequestQuery query, CancellationToken cancellationToken)
        {
            var image = await _imageRepository.GetByIdAsync(query.Id);

            if (image == null) throw new ApiException($"Image Not Found.");

            var response = await _imageService.GetImage(query.Id, image.FileName, query.ImageType, query.CustomSize);
            response.FileName = image.FileName;
            response.ContentType = image.ContentType;

            return new Response<FileDataResponse>(response);
        }
    }
    //}
}
