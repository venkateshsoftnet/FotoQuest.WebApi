using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using FotoQuest.Application.Interfaces.Repositories;
using FotoQuest.Application.Wrappers;
using FotoQuest.Domain.Entities;

using MediatR;

namespace FotoQuest.Application.Features.Images.Commands.SaveImage
{
    public class SaveImageCommand : IRequest<Response<Guid>>
    {
        public string FileName { get; set; }

        public string Description { get; set; }

        public string FileType { get; set; }

        public long FileSize { get; set; }

        public string FileContent { get; set; }
    }
    public class SaveImageCommandCommandHandler : IRequestHandler<SaveImageCommand, Response<Guid>>
    {
        private readonly IImageRepositoryAsync _imageRepository;
        private readonly IMapper _mapper;

        public SaveImageCommandCommandHandler(IImageRepositoryAsync imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(SaveImageCommand request, CancellationToken cancellationToken)
        {
            var image = _mapper.Map<Image>(request);

            try
            {
                image.Id = Guid.NewGuid();

                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "images", image.Id.ToString() + "_" + request.FileName);
                File.WriteAllBytes(fileName, Convert.FromBase64String(request.FileContent));

                await _imageRepository.AddAsync(image);
            }
            catch (Exception ex)
            {
                throw;
            }            

            return new Response<Guid>(image.Id);
        }
    }
}
