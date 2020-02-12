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
        public int IngredientId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter name ingredient  ")]
        public string Name { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter amout ")]
        public double Amout { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter note")]
        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}