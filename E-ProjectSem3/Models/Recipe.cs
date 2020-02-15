using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public int ApproveId { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter title")]
        public string Title { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter description")]
        public string Description { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter content")]
        public string Content { get; set; }
        [DisplayName("FeaturedImage")]
        public string FeaturedImage { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter detail")]
        public string Detail { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter difficulty")]
        public string Difficulty { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter preparation minute ")]
        public int PreparationMinute { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter cooking minute ")]
        public int CookingMinute { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter cooking tempareture ")]
        [DisplayName("Cooking Tempareture")]
        public int CookingTemp { get; set; }
        public string Video { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Nutrition> Nutrition { get; set; }
        public virtual ICollection<Step> Steps { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<ContestUser> ContestUsers { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public enum RecipeStatus
        {
            NonActive = 0,
            Active = 1,
        }
        public enum RecipeType
        {
            NotFree = 0,
            Free = 1,
        }

    }
}