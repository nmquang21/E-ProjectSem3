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
        public int StepId { get; set; }

        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter index")]
        public int Index { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter name step")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter value")]
        public double Value { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter extra information")]
        public string ExtraInfor { get; set; }

        public string ImagePath { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}