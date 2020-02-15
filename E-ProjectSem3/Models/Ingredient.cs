using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter name ingredient")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter amount")]
        public double Amount { get; set; }
        public string Note { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int RecipeId { get; set; }
    }
}