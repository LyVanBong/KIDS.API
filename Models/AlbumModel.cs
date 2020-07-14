using System;

namespace KIDS.API.Models
{
    public class AlbumModel
    {
        public Guid AlbumID { get; set; }
        public Guid ClassID { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }
        public Guid UserCreate { get; set; }
    }
}