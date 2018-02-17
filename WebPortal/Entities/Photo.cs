using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhotographyEye.Entities
{
    [Table("Photos")]
    public class Photo : IEntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
        public virtual Album Album { get; set; }
        public int AlbumId { get; set; }
        public DateTime DateUploaded { get; set; }
    }
}
