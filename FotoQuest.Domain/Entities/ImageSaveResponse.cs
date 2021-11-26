using System;

namespace FotoQuest.Domain.Entities
{
    public class ImageSaveResponse
    {
        public Guid Id { get; set; }

        public bool IsSuccess { get; set; }
    }
}
