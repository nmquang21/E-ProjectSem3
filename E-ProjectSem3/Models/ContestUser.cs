using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_ProjectSem3.Models
{
    public class ContestUser
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual Contest Contest { get; set; }
        public virtual Prize Prizes { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}