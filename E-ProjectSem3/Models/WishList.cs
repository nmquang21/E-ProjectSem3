using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class WishList
    {
        [Key]
        public int WishListId { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}