using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class Prize
    {
        [Key]
        public int Id { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter money")]
        public double Money { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Please enter description")]
        public string Description { get; set; }
        //public virtual Contest Contest { get; set; }
        //public virtual ContestPrize ContestPrize { get; set; }
        public virtual ContestRecipe ContestRecipe { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
