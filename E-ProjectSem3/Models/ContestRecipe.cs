using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class ContestRecipe
    {
       
        public int Id { get; set; }
        public int Score { get; set; } = -1;
        [Required]
        public virtual Recipe Recipe { get; set; }
        
        public virtual Contest Contest { get; set; }
        //public virtual ContestPrize ContestPrize { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }


    }
}