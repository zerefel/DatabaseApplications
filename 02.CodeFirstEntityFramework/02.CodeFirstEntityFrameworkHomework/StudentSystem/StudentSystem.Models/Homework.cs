using System;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{
    using ContentType = StudentSystem.Models.Enums.ContentType;

    public class Homework
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public ContentType ContentType { get; set; }
        public DateTime SubmissionDate { get; set; }    
        public int AuthorId { get; set; }
        public virtual Student Author{ get; set; }
    }
}