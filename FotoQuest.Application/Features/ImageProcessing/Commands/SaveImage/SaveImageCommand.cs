using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using FotoQuest.Application.Interfaces.Repositories;
using FotoQuest.Application.Interfaces.Services;
using FotoQuest.Application.Wrappers;
using FotoQuest.Domain.Entities;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace FotoQuest.Application.Features.Images.Commands.SaveImage
{
    public class SaveImageCommand : IRequest<Response<SaveImageCommandResponse>>
    {
        public List<IFormFile> Files { get; set; }
    }
    public class SaveImageCommandCommandHandler : IRequestHandler<SaveImageCommand, Response<SaveImageCommandResponse>>
    {
        private readonly IImageRepositoryAsync _imageRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public SaveImageCommandCommandHandler(IImageRepositoryAsync imageRepository, IImageService imageService, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<Response<SaveImageCommandResponse>> Handle(SaveImageCommand request, CancellationToken cancellationToken)
        {
            var saveImageCommandResponse = new SaveImageCommandResponse
            {
                Response = new List<ImageSaveResponse>()
            };            

            foreach (var item in request.Files)
            {
                var image = _mapper.Map<Image>(item);

                image.Id = Guid.NewGuid();

                await _imageService.SaveImage(image.Id, item);

                await _imageRepository.AddAsync(image);

                saveImageCommandResponse.Response.Add(new ImageSaveResponse
                {
                    IsSuccess = true,
                    Id = image.Id
                });
            }

            return new Response<SaveImageCommandResponse>(saveImageCommandResponse);
        }
    }
}
