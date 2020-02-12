using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public int ApproveId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your content ")]
        public string Content { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public enum Status
        {
            Active = 1,
            DeActive = 0,
            Deleted = -1
        }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}