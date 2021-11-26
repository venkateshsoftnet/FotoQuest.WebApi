using FotoQuest.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace FotoQuest.Domain.Entities
{
    public class Image : AuditableBaseEntity
    {
        public string FileName { get; set; }

        [NotMapped]
        public string FileContent { get; set; }

        public string FileExtension { get; set; }
    }
}
