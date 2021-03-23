using System;
using System.Collections.Generic;
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

    public class AlbumTicketModel
    {
        public Guid AlbumID { get; set; }
        public Guid ClassID { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }
        public Guid UserCreate { get; set; }
        public List<AlbumDetailTicketModel> AlbumList { get; set; }
    }

    public class AlbumDetailTicketModel
    {
        public Guid ImageID { get; set; }
        public Guid AlbumID { get; set; }
        public String Description { get; set; }
        public String ImageURL { get; set; }
        public Int32 Sort { get; set; }
        public int Action { get; set; } = (int)UserActionAlbum.Update;
    }
    public enum UserActionAlbum 
    {
        Insert,
        Update,
        Delete
    }
}