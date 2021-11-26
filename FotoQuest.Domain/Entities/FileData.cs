using System.IO;

namespace FotoQuest.WebApi.Domain.Entities
{
    public class FileData
    {
        public MemoryStream MemoryStream { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }
    }
}
