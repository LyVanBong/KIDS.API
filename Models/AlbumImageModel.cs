using System;

namespace KIDS.API.Models
{
    public class AlbumImageModel
    {
        public Guid ImageID { get; set; }
        public Guid AlbumID { get; set; }
        public String Description { get; set; }
        public String ImageURL { get; set; }
        public Int32 Sort { get; set; }
    }
}