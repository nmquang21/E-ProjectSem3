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
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your content ")]
        public string Content { get; set; }
        public int Rate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int RecipeId { get; set; }
        public string UserId { get; set; }
        public int ApproveId { get; set; }

        public enum Status
        {
            Active = 1,
            NonActive = 0,
        }
    }
}