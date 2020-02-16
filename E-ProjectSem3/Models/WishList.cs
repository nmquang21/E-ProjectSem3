using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class WishList
    {
        [Key, ForeignKey("Recipe"), Column(Order = 0)]
        public int RecipeId { get; set; }

        [Key, ForeignKey("ApplicationUser"), Column(Order = 1)]
        public string UserId { get; set; }

        public Recipe Recipe { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}