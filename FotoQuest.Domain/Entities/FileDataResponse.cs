using System.IO;

namespace FotoQuest.Domain.Entities
{
    public class FileDataResponse
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public MemoryStream MemoryStream { get; set; }
    }
}
