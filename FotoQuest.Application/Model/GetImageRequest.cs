using System;

using FotoQuest.Application.Wrappers;
using FotoQuest.Domain.Entities;

using MediatR;

namespace FotoQuest.Application.Model
{
    public class GetImageRequestQuery : IRequest<Response<FileDataResponse>>
    {
        public Guid Id { get; set; }

        public ImageType ImageType { get; set; }

        public int CustomSize { get; set; }
    }
}
