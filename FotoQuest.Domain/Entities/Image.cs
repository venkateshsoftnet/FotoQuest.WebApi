using FotoQuest.Domain.Common;

namespace FotoQuest.Domain.Entities
{
    public class Image : AuditableBaseEntity
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public string Length { get; set; }
    }
}
