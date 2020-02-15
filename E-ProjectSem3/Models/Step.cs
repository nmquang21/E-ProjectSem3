using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class Step
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter index")]
        public int Index { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter title step")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int RecipeId { get; set; }
    }
}