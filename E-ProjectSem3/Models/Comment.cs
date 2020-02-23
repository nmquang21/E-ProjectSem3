using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class Comment
    {
        [Key, ForeignKey("Recipe"), Column(Order = 0)]
        public int RecipeId { get; set; }
        [Key, ForeignKey("ApplicationUser"), Column(Order = 1)]
        public string UserId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your content ")]
        public string Content { get; set; }
        public int Rate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public string ApproveId { get; set; }
        public int Status { get; set; }

        public enum StatusComment
        {
            Active = 1,
            NonActive = 0,
        }
    }
}