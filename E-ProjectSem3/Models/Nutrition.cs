using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class Nutrition
    {
        [Key]
        public int NutritionId { get; set; }
        public virtual Recipe Recipe { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter description step")]
        [DisplayName(" Step Description")]
        public string StepDescription { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter image step")]
        [DisplayName(" Step Image")]
        public string StepImage { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}