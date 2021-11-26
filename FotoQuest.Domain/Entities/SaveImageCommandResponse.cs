using System.Collections.Generic;

namespace FotoQuest.Domain.Entities
{
    public class SaveImageCommandResponse
    {
        public List<ImageSaveResponse> Response { get; set; }
    }
}
