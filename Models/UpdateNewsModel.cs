using System;

namespace KIDS.API.Models
{
    public class UpdateNewsModel
    {
        public Guid NewsId { get; set; }
        public Guid ClassId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreate { get; set; }
        public Guid UserCreate { get; set; }
    }
}