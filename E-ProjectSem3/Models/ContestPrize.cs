using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class ContestPrize
    {
        public int Id { get; set; }
        [Required]
        public virtual Prize Prize { get; set; }
        [Required]
        public virtual ContestRecipe ContestRecipe { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


    }
}