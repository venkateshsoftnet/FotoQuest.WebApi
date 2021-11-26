using FotoQuest.WebApi.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace FotoQuest.WebApi.Domain.Entities
{
    public class Image : AuditableBaseEntity
    {
        public string FileName { get; set; }

        [NotMapped]
        public string FileContent { get; set; }

        public string FileExtension { get; set; }
    }
}
